using System;
using System.Globalization;
using System.Threading;
using System.Web;
using System.Web.UI.WebControls;

namespace Microsoft.Exchange.Management.ControlPanel.WebControls
{
	// Token: 0x0200064B RID: 1611
	public class RtlUtil
	{
		// Token: 0x1700271B RID: 10011
		// (get) Token: 0x06004656 RID: 18006 RVA: 0x000D4BC9 File Offset: 0x000D2DC9
		public static string DirectionMark
		{
			get
			{
				if (!RtlUtil.IsRtl)
				{
					return "&#x200E;";
				}
				return "&#x200F;";
			}
		}

		// Token: 0x1700271C RID: 10012
		// (get) Token: 0x06004657 RID: 18007 RVA: 0x000D4BDD File Offset: 0x000D2DDD
		public static string SearchDefaultMock
		{
			get
			{
				if (!RtlUtil.IsRtl)
				{
					return "▶";
				}
				return "◀";
			}
		}

		// Token: 0x1700271D RID: 10013
		// (get) Token: 0x06004658 RID: 18008 RVA: 0x000D4BF1 File Offset: 0x000D2DF1
		public static string DecodedDirectionMark
		{
			get
			{
				if (!RtlUtil.IsRtl)
				{
					return RtlUtil.DecodedLtrDirectionMark;
				}
				return RtlUtil.DecodedRtlDirectionMark;
			}
		}

		// Token: 0x1700271E RID: 10014
		// (get) Token: 0x06004659 RID: 18009 RVA: 0x000D4C08 File Offset: 0x000D2E08
		public static bool IsRtl
		{
			get
			{
				CultureInfo currentUICulture = Thread.CurrentThread.CurrentUICulture;
				return currentUICulture != null && currentUICulture.TextInfo.IsRightToLeft;
			}
		}

		// Token: 0x0600465A RID: 18010 RVA: 0x000D4C30 File Offset: 0x000D2E30
		public static string ConvertToBidiString(string value, bool isRtl)
		{
			string newValue = isRtl ? RtlUtil.rdmWithLeftParenthesis : RtlUtil.ldmWithLeftParenthesis;
			string newValue2 = isRtl ? RtlUtil.rdmWithRightParenthesis : RtlUtil.ldmwithRightParenthesis;
			return value.Replace("(", newValue).Replace(")", newValue2);
		}

		// Token: 0x0600465B RID: 18011 RVA: 0x000D4C74 File Offset: 0x000D2E74
		public static string ConvertToDecodedBidiString(string value, bool isRtl)
		{
			string newValue = isRtl ? RtlUtil.decodedRDMWithLeftParenthesis : RtlUtil.decodedLDMWithLeftParenthesis;
			string newValue2 = isRtl ? RtlUtil.decodedRDMWithRightParenthesis : RtlUtil.decodedLDMwithRightParenthesis;
			return value.Replace("(", newValue).Replace(")", newValue2);
		}

		// Token: 0x1700271F RID: 10015
		// (get) Token: 0x0600465C RID: 18012 RVA: 0x000D4CB8 File Offset: 0x000D2EB8
		public static HorizontalAlign LeftAlign
		{
			get
			{
				if (!RtlUtil.IsRtl)
				{
					return HorizontalAlign.Left;
				}
				return HorizontalAlign.Right;
			}
		}

		// Token: 0x17002720 RID: 10016
		// (get) Token: 0x0600465D RID: 18013 RVA: 0x000D4CC4 File Offset: 0x000D2EC4
		public static HorizontalAlign RightAlign
		{
			get
			{
				if (!RtlUtil.IsRtl)
				{
					return HorizontalAlign.Right;
				}
				return HorizontalAlign.Left;
			}
		}

		// Token: 0x0600465E RID: 18014 RVA: 0x000D4CD0 File Offset: 0x000D2ED0
		public static HorizontalAlign GetHorizontalAlign(HorizontalAlign value)
		{
			HorizontalAlign result = value;
			switch (value)
			{
			case HorizontalAlign.Left:
				result = (RtlUtil.IsRtl ? HorizontalAlign.Right : HorizontalAlign.Left);
				break;
			case HorizontalAlign.Right:
				result = (RtlUtil.IsRtl ? HorizontalAlign.Left : HorizontalAlign.Right);
				break;
			}
			return result;
		}

		// Token: 0x04002F8F RID: 12175
		public const string LtrDirectionMark = "&#x200E;";

		// Token: 0x04002F90 RID: 12176
		public const string RtlDirectionMark = "&#x200F;";

		// Token: 0x04002F91 RID: 12177
		public static readonly string DecodedLtrDirectionMark = HttpUtility.HtmlDecode("&#x200E;");

		// Token: 0x04002F92 RID: 12178
		public static readonly string DecodedRtlDirectionMark = HttpUtility.HtmlDecode("&#x200F;");

		// Token: 0x04002F93 RID: 12179
		private static readonly string ldmWithLeftParenthesis = "&#x200E;(";

		// Token: 0x04002F94 RID: 12180
		private static readonly string ldmwithRightParenthesis = ")&#x200E;";

		// Token: 0x04002F95 RID: 12181
		private static readonly string rdmWithLeftParenthesis = "&#x200F;(";

		// Token: 0x04002F96 RID: 12182
		private static readonly string rdmWithRightParenthesis = ")&#x200F;";

		// Token: 0x04002F97 RID: 12183
		private static readonly string decodedLDMWithLeftParenthesis = RtlUtil.DecodedLtrDirectionMark + "(";

		// Token: 0x04002F98 RID: 12184
		private static readonly string decodedLDMwithRightParenthesis = ")" + RtlUtil.DecodedLtrDirectionMark;

		// Token: 0x04002F99 RID: 12185
		private static readonly string decodedRDMWithLeftParenthesis = RtlUtil.DecodedRtlDirectionMark + "(";

		// Token: 0x04002F9A RID: 12186
		private static readonly string decodedRDMWithRightParenthesis = ")" + RtlUtil.DecodedRtlDirectionMark;
	}
}
