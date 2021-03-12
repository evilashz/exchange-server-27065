using System;
using System.Globalization;

namespace Microsoft.Exchange.Management.IisTasks
{
	// Token: 0x02000411 RID: 1041
	internal sealed class IsapiExtension
	{
		// Token: 0x06002458 RID: 9304 RVA: 0x00090C70 File Offset: 0x0008EE70
		public IsapiExtension(string physicalPath, string groupID, string description, bool allow, bool uiDeletable)
		{
			if (physicalPath == null)
			{
				throw new IsapiExtensionMustHavePhysicalPathException();
			}
			this.physicalPath = physicalPath;
			this.groupID = ((groupID != null) ? groupID : "");
			this.description = ((description != null) ? description : "");
			this.allow = allow;
			this.uiDeletable = uiDeletable;
		}

		// Token: 0x06002459 RID: 9305 RVA: 0x00090CC8 File Offset: 0x0008EEC8
		internal static IsapiExtension Parse(string extensionString)
		{
			bool flag = false;
			string text = null;
			string text2 = null;
			string[] array = extensionString.Split(new char[]
			{
				','
			});
			switch (array.Length)
			{
			case 0:
				return null;
			case 1:
				return null;
			case 2:
				goto IL_5D;
			case 3:
				goto IL_53;
			case 4:
				break;
			default:
				text2 = array[4];
				break;
			}
			text = array[3];
			IL_53:
			flag = IsapiExtension.IntStringToBoolean(array[2]);
			IL_5D:
			string text3 = array[1];
			bool flag2 = IsapiExtension.IntStringToBoolean(array[0]);
			return new IsapiExtension(text3, text, text2, flag2, flag);
		}

		// Token: 0x0600245A RID: 9306 RVA: 0x00090D54 File Offset: 0x0008EF54
		private static bool IntStringToBoolean(string intString)
		{
			int num = 0;
			if (!int.TryParse(intString, NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite, CultureInfo.InvariantCulture, out num))
			{
				IisTaskTrace.IisUtilityTracer.TraceError<string>(0L, "One of the ISAPI extension booleans could not be parsed by Int32.TryParse(); using false", intString);
				return false;
			}
			return num != 0;
		}

		// Token: 0x0600245B RID: 9307 RVA: 0x00090D90 File Offset: 0x0008EF90
		internal string ToMetabaseString()
		{
			return string.Format(CultureInfo.InvariantCulture, "{0},{1},{2},{3},{4}", new object[]
			{
				this.allow ? 1 : 0,
				this.physicalPath,
				this.uiDeletable ? 1 : 0,
				this.groupID,
				this.description
			});
		}

		// Token: 0x17000AB8 RID: 2744
		// (get) Token: 0x0600245C RID: 9308 RVA: 0x00090DF7 File Offset: 0x0008EFF7
		// (set) Token: 0x0600245D RID: 9309 RVA: 0x00090DFF File Offset: 0x0008EFFF
		public bool Allow
		{
			get
			{
				return this.allow;
			}
			set
			{
				this.allow = value;
			}
		}

		// Token: 0x17000AB9 RID: 2745
		// (get) Token: 0x0600245E RID: 9310 RVA: 0x00090E08 File Offset: 0x0008F008
		// (set) Token: 0x0600245F RID: 9311 RVA: 0x00090E10 File Offset: 0x0008F010
		public string PhysicalPath
		{
			get
			{
				return this.physicalPath;
			}
			set
			{
				this.physicalPath = value;
			}
		}

		// Token: 0x17000ABA RID: 2746
		// (get) Token: 0x06002460 RID: 9312 RVA: 0x00090E19 File Offset: 0x0008F019
		// (set) Token: 0x06002461 RID: 9313 RVA: 0x00090E21 File Offset: 0x0008F021
		public bool UIDeletable
		{
			get
			{
				return this.uiDeletable;
			}
			set
			{
				this.uiDeletable = value;
			}
		}

		// Token: 0x17000ABB RID: 2747
		// (get) Token: 0x06002462 RID: 9314 RVA: 0x00090E2A File Offset: 0x0008F02A
		// (set) Token: 0x06002463 RID: 9315 RVA: 0x00090E32 File Offset: 0x0008F032
		public string GroupID
		{
			get
			{
				return this.groupID;
			}
			set
			{
				this.groupID = value;
			}
		}

		// Token: 0x17000ABC RID: 2748
		// (get) Token: 0x06002464 RID: 9316 RVA: 0x00090E3B File Offset: 0x0008F03B
		// (set) Token: 0x06002465 RID: 9317 RVA: 0x00090E43 File Offset: 0x0008F043
		public string Description
		{
			get
			{
				return this.description;
			}
			set
			{
				this.description = value;
			}
		}

		// Token: 0x04001CDF RID: 7391
		private string physicalPath;

		// Token: 0x04001CE0 RID: 7392
		private string groupID;

		// Token: 0x04001CE1 RID: 7393
		private string description;

		// Token: 0x04001CE2 RID: 7394
		private bool allow;

		// Token: 0x04001CE3 RID: 7395
		private bool uiDeletable;
	}
}
