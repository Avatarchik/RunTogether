using UnityEngine;

namespace Datas
{
    public class DataManager : MonoBehaviour
    {
        private static DataManager INSTANCE;
        public static DataManager Instance
        {
            get
            {
                if (INSTANCE==null)
                {
                    INSTANCE = FindObjectOfType<DataManager>();
                    #if UNITY_EDITOR
                    INSTANCE.Awake();
                    #endif
                }
                return INSTANCE;
            }
        }


        public UserDatas GetUserData;// { get; private set; }

        public void InitUserData(UserDatas _userdata)
        {
            GetUserData = _userdata;
        }


        internal void Awake()
        {
            InitUserData(new UserDatas("https://tinygames.oss-cn-shanghai.aliyuncs.com/WechatIMG1.jpeg","Change the world!","NSWell"));
        }
    }
}
