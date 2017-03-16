using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleField : MonoBehaviour
{
    public class Group
    {
        public NX_List<Form> m_forms = new NX_List<Form>();

        public int m_group;
        public Vector3 m_attackDir;
        public int m_xNumber = 3;
        public int m_yNumber = 3;

        public Vector3 m_initPos;


        public Group(int group, Vector3 attackDir)
        {
            m_group = group;
            m_attackDir = attackDir;
            m_initPos = attackDir * -10;
        }
        public Group(int group, Vector3 attackDir,int xNumber,int yNumber)
        {
            m_group = group;
            m_attackDir = attackDir;
            m_initPos = attackDir * -10;

            m_xNumber = Mathf.Max(xNumber, 1);
            m_yNumber = Mathf.Max(yNumber, 1);
        }

        public int m_MaxNumber
        {
            get { return m_xNumber * m_yNumber; }
        }

        public void AddForm(Form form)
        {
            if (m_forms.Count > m_MaxNumber)
                return;
            m_forms.Add(form);
            Debug.Log(string.Format("玩家{0}添加了一个小队单位，当前数量为{1}", m_group, m_forms.Count));

            form.m_group = m_group;
            form.m_attackDir = m_attackDir;
            form.m_initPos = GetFormPos(m_forms.List.Count -1);
        }
        public void CreateForm(Form form,int posIndex)
        {
            form.m_group = m_group;
            form.m_attackDir = m_attackDir;
            form.m_initPos = GetFormPos(posIndex % m_MaxNumber);

            form.FillForm();
        }

        public void FillGroup()
        {
            for (int i = 0; i < m_forms.Count; i++)
            {
                m_forms.List[i].FillForm();
            }
        }

        public Vector3 GetFormPos(int index)
        {
            index = Mathf.Max(index, 0);
            Vector3 pos;
            int y = index / m_yNumber;//第几列
            int x = index % m_yNumber;//第几行
            pos = m_initPos + x * Vector3.forward * 5 + y * m_attackDir * -5;
            Debug.Log(string.Format("序号{0}处于 第{1}列 第{2}行，位置{3}", index,x, y, pos));
            return pos;
        }
    }



    public class Form
    {
        public int m_id;
        public Vector3 m_initPos;

        public int m_group;
        public Vector3 m_attackDir;

        public Form(int id)
        {
            m_id = id;
        }
        public void FillForm()
        {
            Character.Prop prop;
            Vector3[] posOffsets = new Vector3[] {m_attackDir * -0.5f, Vector3.forward, Vector3.back};//只有三个小弟的位置
            if (CharacterPropInfo.TryGetInfo(m_id, out prop))
            {
                string characterPath = "";
                Object Prefab = Resources.Load(characterPath + prop.m_modelId, typeof(GameObject));
                //Debug.Log("ResourcesPath:" + characterPath + prop.m_modelId);

                Create((GameObject)Prefab, m_initPos + m_attackDir, prop);
            }
            if (prop.m_IsBoss == 1)
            {
                Character.Prop armyProp;
                if (CharacterPropInfo.TryGetInfo(prop.m_ArmyId, out armyProp))
                {
                    string characterPath = "";
                    Object Prefab = Resources.Load(characterPath + prop.m_ArmyId, typeof(GameObject));
                    for (int i = 0; i < prop.m_ArmyCount; i++)
                    {
                        if (i >= posOffsets.Length)
                            break;
                        Create((GameObject)Prefab, m_initPos + posOffsets[i], armyProp);
                    }
                }
            }
            
        }

        public void Create(GameObject Prefab, Vector3 pos, Character.Prop prop)
        {
            GameObject characterGo;
            characterGo = GameObject.Instantiate(Prefab) as GameObject;

            characterGo.transform.position = pos;
            Character character = characterGo.AddComponent<Character>();
            character.m_prop = prop;
            character.m_group = m_group;
            character.m_attackDir = m_attackDir;
        }
    }

    public static BattleField Instance;
    public int m_testIndex;

    int m_maxIndex = 4;
    public NX_List<Form> m_forms = new NX_List<Form>();
    public NX_List<ICharacter> m_arms = new NX_List<ICharacter>();
    public NX_List<ICharacter> m_enemys = new NX_List<ICharacter>();
    private Group m_group1;
    private Group m_group2;
    private NX_Timer m_timer = new NX_Timer();

    public void Init(Group g1, Group g2)
    {
        m_group1 = g1;
        m_group2 = g2;

        g1.FillGroup();
        g2.FillGroup();
    }

    /// <summary>
    /// 竞技场势力，分左右两边
    /// 每边长宽为3*3 或者5*5；
    /// 每个阵型站据 大概 3 * 3 的范围
    /// 
    /// </summary>
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    void Start()
    {
        PrepareTest();
        Invoke("StartBattle", 0);
    }

    void StartBattle()
    {
        _isStart = true;
    }

    void Update()
    {
        
    }

    void OnGUI()
    {
        GUILayout.BeginHorizontal();
        m_testIndex = (int)GUILayout.HorizontalSlider(m_testIndex, 0, m_maxIndex, GUILayout.Width(200));
        GUILayout.Label("" + m_testIndex);
        GUILayout.EndHorizontal();
        if (GUILayout.Button("ChangeScene"))
        {
            PlayerPrefs.SetInt("BattleSceneIndex", m_testIndex);
            SceneManager.LoadScene("GameMain");
        }
    }

    public Transform FindCloseEnemy(Vector3 aPos, int friendGroup)
    {

        float minDis = 10000;
        Transform cur = null;
        switch (friendGroup)
        {
            case 1:
                if (m_enemys.List == null || m_enemys.Count == 0)
                    return null;
                for (int i = 0; i < m_enemys.Count; i++)
                {
                    Character c = (Character)m_enemys.List[i];
                    float dis = Vector3.Distance(c.transform.position, aPos);
                    if (dis < minDis)
                    {
                        cur = c.transform;
                        minDis = dis;
                    }
                }
                break;
            case 2:
                if (m_arms.List == null || m_arms.Count == 0)
                    return null;
                for (int i = 0; i < m_arms.Count; i++)
                {
                    Character c = (Character)m_arms.List[i];
                    float dis = Vector3.Distance(c.transform.position, aPos);
                    if (dis < minDis)
                    {
                        cur = c.transform;
                        minDis = dis;
                    }
                }
                break;
        }
        return cur;
    }

    void PrepareTest()
    {
        Group g1 = new Group(1, Vector3.right);
        Group g2 = new Group(2, Vector3.left);
        m_testIndex =  PlayerPrefs.GetInt("BattleSceneIndex");
        switch (m_testIndex)
        {
            case 0:
                {
                    g1.AddForm(new Form(1102));
                    g1.AddForm(new Form(1102));
                    g1.AddForm(new Form(1102));

                    g1.AddForm(new Form(1101));
                    g1.AddForm(new Form(1101));
                    g1.AddForm(new Form(1104));

                    g1.AddForm(new Form(1103));
                    g1.AddForm(new Form(1103));
                    g1.AddForm(new Form(1104));


                    g2.AddForm(new Form(1102));
                    g2.AddForm(new Form(1102));
                    g2.AddForm(new Form(1102));

                    g2.AddForm(new Form(1101));
                    g2.AddForm(new Form(1101));
                    g2.AddForm(new Form(1104));

                    g2.AddForm(new Form(1103));
                    g2.AddForm(new Form(1103));
                    g2.AddForm(new Form(1104));
                }
                break;
            case 1:
                g1.AddForm(new Form(1101));

                g2.AddForm(new Form(1103));
                break;
            case 2:
                g1.AddForm(new Form(1101));
                g1.AddForm(new Form(1102));

                g2.AddForm(new Form(1103));
                g2.AddForm(new Form(1104));
                break;
            case 3:
                g1.AddForm(new Form(1101));
                g1.AddForm(new Form(1101));
                g1.AddForm(new Form(1101));

                g1.AddForm(new Form(1103));
                g1.AddForm(new Form(1103));
                g1.AddForm(new Form(1103));


                g2.AddForm(new Form(1102));
                g2.AddForm(new Form(1102));
                g2.AddForm(new Form(1102));

                g2.AddForm(new Form(1104));
                g2.AddForm(new Form(1104));
                g2.AddForm(new Form(1104));
                break;
            case 4:
                g1.AddForm(new Form(1101));
                g1.AddForm(new Form(1101));
                g1.AddForm(new Form(1101));

                g1.AddForm(new Form(1102));
                g1.AddForm(new Form(1102));
                g1.AddForm(new Form(1102));


                g2.AddForm(new Form(1103));
                g2.AddForm(new Form(1103));
                g2.AddForm(new Form(1103));

                g2.AddForm(new Form(1104));
                g2.AddForm(new Form(1104));
                g2.AddForm(new Form(1104));
                break;
        }
        //Test
        Init(g1, g2);
    }
    public void Add(ICharacter c, int group)
    {
        switch (group)
        {
            case 1:
                m_arms.Add(c);
                break;
            case 2:
                m_enemys.Add(c);
                break;
        }
    }
    public void Remove(ICharacter c, int group)
    {
        switch (group)
        {
            case 1:
                m_arms.Remove(c);
                break;
            case 2:
                m_enemys.Remove(c);
                break;
        }
    }

    private bool _isStart;
    public bool IsStart()
    {
        return _isStart;
    }
    public bool IsEnd()
    {
        if (_isStart && (m_arms.Count == 0 || m_enemys.Count == 0))
        {
            return true;
        }
        return false;
    }
}
