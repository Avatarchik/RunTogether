namespace Datas
{
    [System.Serializable]
    public class RequestUserData
    {
        public int id;
        public string nickname;
        public string headimages;
        public string phone;
        public string password;
        public string createtime;
        public string updatetime;
        public string address;
    }

    [System.Serializable]
    public class RequestUserRespon
    {
        public RequestUserData data;
        public int code;
        public string msg;
    }
}