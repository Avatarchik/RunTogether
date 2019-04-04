using UnityEngine;

namespace Datas
{
    public class AppManager : MonoBehaviour
    {
        private static AppManager INSTANCE;
        public static AppManager Instance
        {
            get
            {
                if (INSTANCE == null)
                {
                    INSTANCE = FindObjectOfType<AppManager>();
                }
                return INSTANCE;
            }
        }


        public UserDatas GetUserData;// { get; private set; }

        public void InitUserData(UserDatas _userdata)
        {
            GetUserData = _userdata;
        }



        public bool WasLogined { get; private set; }
        public void SetLoginState(bool loginState=false)
        {
            WasLogined = loginState;
        }
    }
}
