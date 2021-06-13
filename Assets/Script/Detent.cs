using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detent : MonoBehaviour
{   
    class Point
    {
        public static Point left;
        public static Point right;

        public bool active { get { return root == null ? false : root.activeSelf; } }
        public GameObject root;
        public HingeJoint2D joint;
        public HingeJoint2D joint2;
        public Rigidbody2D rigidbody;

        public Point(GameObject obj)
        {

            root = obj;
            HingeJoint2D[] joints = obj.GetComponents<HingeJoint2D>();
            joint = joints[0];
            joint2 = joints[1];
            rigidbody = obj.GetComponent<Rigidbody2D>();
        }
        public void Init(string name, Vector3 pos, Quaternion qua)
        {

            root.name = name;
            root.transform.position = pos;
            root.transform.rotation = qua;
            rigidbody.velocity = Vector2.zero;

            joint2.enabled = false;
            Show();
        }
        public void Show(bool show = true)
        {
            if (show != root.activeSelf) root.SetActive(show);
        }
    }

    public Transform content;                       
    private List<Point> mem = new List<Point>();    
    public SpriteRenderer[] prefabs;        
                                            
    public bool[] HangSegment;				
    public bool useBendLimit = false;		
    public int bendLimit = 45;				
    public bool useBreakForce = false;		
    public float BreakForce = 100;          
    private Vector2 leftP;                  
    private Vector2 rightP;                 
    private Vector2 rightOff;				
    [Range(-0.5f, 0.5f)] public float overlapFactor;	
    [Range(0, 0.5f)] public float minError = 0.25f;     
    public int maxLength = 50;                          

    float segmentHeight;        
    float yScale;               

    private void Awake() { Init(); }

    public void Init()
    {
        yScale = prefabs[0].transform.localScale.y;
        segmentHeight = prefabs[0].bounds.size.y * (1 + overlapFactor);
    }

    public void DrawLength(Vector2 l, Vector2 r)
    {

        if (leftP != l || (rightP + rightOff) != r)
        {

            leftP = l;
            rightP = r;
            Debug.DrawLine(leftP, rightP);
        }
        else return;

        int i = 0;
        float distance = Vector2.Distance(rightP, leftP);
        int segmentCount = (int)(distance / segmentHeight);
        float error = distance - segmentCount * segmentHeight;
        bool fixError = error > minError * segmentHeight || segmentCount == 0;

        int length = segmentCount;
        if (maxLength <= segmentCount)
        {
            length = maxLength;
            fixError = false;
            rightOff = rightP - ((rightP - leftP).normalized * maxLength * segmentHeight + leftP);
            rightP -= rightOff;
        }
        else
        {
            rightOff = Vector2.zero;
        }

        for (; i < length; i++) AddPoint(i);

        if (fixError) AddPoint(i++, true);

        if (mem.Count > 0 && i > 0) Point.right = mem[i - 1];

        for (; i < mem.Count; i++) mem[i].Show(false);

        if (!HangSegment[0])
        {
            Point.left.joint.enabled = false;
        }
        if (HangSegment[1])
        {

            Point.right.joint2.autoConfigureConnectedAnchor = false;
            Point.right.joint2.connectedBody = null;
            if (fixError)
            {
                Point.right.joint2.anchor = Point.right.root.transform.InverseTransformPoint(rightP);
            }
            else
            {
                Point.right.joint2.anchor = new Vector2(0, segmentHeight / 2);
            }
            Point.right.joint2.connectedAnchor = rightP;
            Point.right.joint2.enabled = true;
        }
    }

    public void Clear()
    {

        int i = 0, sum = mem.Count;
        for (; i < sum; i++) if (!mem[i].active) break;

        for (int j = i; j < sum; j++)
        {
            Destroy(mem[i].root);
            mem.RemoveAt(i);
        }
    }

    private void AddPoint(int i, bool fix = false)
    {

        Point point;
        if (i < mem.Count)
        {
            point = mem[i];
        }
        else
        {
            GameObject obj = Instantiate(prefabs[i % 2], content).gameObject;
            point = new Point(obj);
            mem.Add(point);
        }

        float theta = Mathf.Atan2(rightP.y - leftP.y, rightP.x - leftP.x);

        float dtheta = theta * Mathf.Rad2Deg;
        if (dtheta > 180) dtheta -= 360;
        else if (dtheta < -180) dtheta += 360;


        float dx = segmentHeight * Mathf.Cos(theta);
        float dy = segmentHeight * Mathf.Sin(theta);
        float startX = leftP.x + dx / 2;
        float startY = leftP.y + dy / 2;

        point.Init(
            "Segment_" + i + (fix ? "_fix" : ""),
            new Vector3(startX + dx * i, startY + dy * i),
            Quaternion.Euler(0, 0, theta * Mathf.Rad2Deg - 90)
        );

        if (i == 0)
        {
            Point.left = point;
            point.joint.connectedAnchor = leftP;
            point.joint.anchor = new Vector2(0, -segmentHeight / 2);
        }
        else
        {
            AddJoint(dtheta, segmentHeight / yScale, i - 1, point);
        }
    }

    private void AddJoint(float dthetaT, float segmentHeightT, int index, Point point)
    {
        point.joint.connectedBody = mem[index].rigidbody;
        point.joint.anchor = new Vector2(0, -segmentHeightT / 2);
        point.joint.connectedAnchor = new Vector2(0, segmentHeightT / 2);
    }

    private void LimitAndBreak(float dthetaT, Point point)
    {

        if (useBendLimit)
        {
            point.joint.useLimits = true;
            point.joint.limits = new JointAngleLimits2D()
            {
                min = dthetaT - bendLimit,
                max = dthetaT + bendLimit
            };
        }
        if (useBreakForce) point.joint.breakForce = BreakForce;
    }
}
