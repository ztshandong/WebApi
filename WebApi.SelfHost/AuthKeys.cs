using System;

namespace WebApi.SelfHost
{
    internal  class AuthKeys
    {
        internal const string _AuthFileKEY = "HX$'GJ3gy&C?`J`TZQ4,F$+vB:k9cuEf_f;4EBl_88AT^-21dU)ow/`hw2/zyWXy";
        internal const string _ROUTE_HELP_GET_COMMON_KEY = "^%qvdi`oRUv,{ssK(79J+|w$@k}){AvC";
        internal const string _ROUTE_HELP_GET_COMMON_SALT = "c^JlTnUEL|GA&2vp64.@Amsa;09|qaG;";
        internal const string _RijndaelProKey = "wO6=0D{I%eO(K:}7RGL6l_Qw-3+%`dj.Cc;%ni||hYhK&IW'v0eXizLA|%6`XmLQ^'ux|o/zIM>Q)6UtA4x`|PT2TE'Mb1u^dTP}eo(6f)eZK_W@9(PS=PRycr)NMqoG";
        internal const string _RijndaelProIV = "(s?Y&}7,^rpS/soZJ574oBi9Fzj,4+P.FhK&xo%VcpG`|fC&UXe*6.8ua+jv*q(e'FcW,NskvE@|^lbq`HfzU+(W9fUR5y;;MskL2+]7v^DV@9A=X[yqawM_$JMV]%f`";
        internal static string _AuthFilePath = AppDomain.CurrentDomain.BaseDirectory + @"\Core.rac";
        internal static string _PrivatekeyPath = AppDomain.CurrentDomain.BaseDirectory + @"\PrivateKey.xml";
        internal static string _PublicKeyPath = AppDomain.CurrentDomain.BaseDirectory + @"\PublicKey.xml";

    }
}
