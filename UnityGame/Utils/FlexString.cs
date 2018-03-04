using System.Text;
using System.Collections.Generic;
using System;

namespace UnityGameToolkit
{
    /// <summary>
    /// FlexString类的解析
    /// </summary>
    /// <example>
    /// <code lang="C#">
    /// FlexString str = new FlexString("收件人：[{@0:收件人}]，类型：{@1:类型}，序号：{@2:留言序号}");
    /// string[] paras=new string[3];
    /// paras[0]="test";
    /// paras[1]="活动物品奖励";
    /// paras[2]="20070920";
    /// for(int i=0;i&lt;3;i++);
    /// {
    ///     str[i]=paras[i];
    /// }
    /// Console.WriteLine("{0}",str.ToString());
    /// </code>
    /// </example>
    public class FlexString
    {
        public class Para
        {
            int startIndex;
            int endIndex;
            int commentIndex;
            int count;
            int commentCount;
            object val;

            public Para(int startIndex, int endIndex, int commentIndex)
            {
                this.startIndex = startIndex;
                this.endIndex = endIndex;
                this.commentIndex = commentIndex;

                count = this.endIndex - this.startIndex + 1;
                if (this.commentIndex != -1)
                {
                    commentCount = this.endIndex - this.commentIndex - 1;
                }
            }

            public object Val
            {
                get
                {
                    return val;
                }
                set
                {
                    val = value;
                }
            }

            public int StartIndex
            {
                get
                {
                    return this.startIndex;
                }
            }

            public int EndIndex
            {
                get
                {
                    return this.endIndex;
                }
            }

            public int CommentIndex
            {
                get
                {
                    return this.commentIndex;
                }
            }

            public int Count
            {
                get
                {
                    return this.count;
                }
            }

            public int CommentCount
            {
                get
                {
                    return this.commentCount;
                }
            }
        }


        private System.Collections.Generic.List<Para> paras = new List<Para>();

        public int ParasCount
        {
            get
            {
                return paras.Count;
            }
        }

        public string GetParaComment(int index)
        {
            Para para = paras[index];
            if (para.CommentCount <= 0)
            {
                return "";
            }

            return format.Substring(para.CommentIndex, para.CommentCount);
        }

        public string GetParaVal(int index)
        {
            Para para = paras[index];
            if (para.Val == null)
            {
                return "";
            }

            return para.Val.ToString();
        }

        private string format;

        public string Format
        {
            get
            {
                return format;
            }
        }

        public object this[int index]
        {
            get
            {
                return paras[index].Val;
            }
            set
            {
                paras[index].Val = value;

            }
        }

        public void SetParams(List<object> replaces)
        {
            for (int i = 0; i < replaces.Count; ++i)
            {
                this[i] = replaces[i];
            }
        }

        public FlexString(string format)
        {
            this.format = format;


            try
            {
                ParseFormat();

            }
            catch (Exception)
            {
                throw new System.ApplicationException("(FlexString) format string's format err");
            }
        }

        private void ParseFormat()
        {
            int startIndex = 0;
            int endIndex = -1;
            int commentIndex = 0;
            int para;
            while ((startIndex = format.IndexOf("{@", endIndex + 1)) != -1)
            {
                if (startIndex + 4 > format.Length)
                {
                    throw new System.ApplicationException();
                }

                if (!int.TryParse(format.Substring(startIndex + 2, 1), out para))
                {
                    throw new System.ApplicationException();
                }

                endIndex = format.IndexOf("}", startIndex + 3);
                if (endIndex == -1)
                {
                    throw new System.ApplicationException();
                }

                commentIndex = format.IndexOf(":", startIndex + 3, endIndex - startIndex - 3);

                paras.Add(new Para(startIndex, endIndex, commentIndex));
            }
        }

        public override string ToString()
        {
            int count = ParasCount;
            if (count < 1)
            {
                return format;
            }

            StringBuilder res = new StringBuilder();

            for (int i = 0; i < count; ++i)
            {
                if (i == 0)
                {
                    res.Append(format.Substring(0, paras[0].StartIndex));
                }

                res.Append(paras[i].Val == null ? "" : paras[i].Val);

                if (i == count - 1)
                {
                    res.Append(format.Substring(paras[count - 1].EndIndex + 1));
                }
                else
                {
                    res.Append(format.Substring(paras[i].EndIndex + 1,
                            paras[i + 1].StartIndex - paras[i].EndIndex - 1));
                }
            }

            return res.ToString();
        }

    }
}


