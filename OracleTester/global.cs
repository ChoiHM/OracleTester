using System;
using System.Reflection;
using System.Windows.Forms;

namespace OracleTester
{
    public static class global
    {
        public static bool ApplicationStop = false;

        // 버전 Property 리턴
        public static string Version
        {
            get
            {
                return $"[Build {Properties.Resources.build.Replace(Environment.NewLine, "")}]";
            }
        }

        public static void DoubleBuffered(this SplitContainer ctrl, bool setting = true)
        {
            Type dgvType = ctrl.GetType();
            PropertyInfo pi = dgvType.GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
            pi.SetValue(ctrl, setting, null);
        }

        public static void DoubleBuffered(this DataGridView ctrl, bool setting = true)
        {
            Type dgvType = ctrl.GetType();
            PropertyInfo pi = dgvType.GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
            pi.SetValue(ctrl, setting, null);
        }

    }
}
