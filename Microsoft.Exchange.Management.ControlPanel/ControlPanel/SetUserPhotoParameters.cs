using System;
using System.IO;
using System.Management.Automation;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020002EC RID: 748
	[DataContract]
	public class SetUserPhotoParameters : WebServiceParameters
	{
		// Token: 0x17001E20 RID: 7712
		// (get) Token: 0x06002D17 RID: 11543 RVA: 0x0008A358 File Offset: 0x00088558
		// (set) Token: 0x06002D18 RID: 11544 RVA: 0x0008A37C File Offset: 0x0008857C
		[DataMember]
		public string Identity
		{
			get
			{
				object obj = base["Identity"];
				if (obj != null)
				{
					return (string)obj;
				}
				return null;
			}
			set
			{
				base["Identity"] = value;
			}
		}

		// Token: 0x17001E21 RID: 7713
		// (get) Token: 0x06002D19 RID: 11545 RVA: 0x0008A38C File Offset: 0x0008858C
		// (set) Token: 0x06002D1A RID: 11546 RVA: 0x0008A3B0 File Offset: 0x000885B0
		public Stream PhotoStream
		{
			get
			{
				object obj = base["PictureStream"];
				if (obj != null)
				{
					return (Stream)obj;
				}
				return null;
			}
			set
			{
				base["PictureStream"] = value;
			}
		}

		// Token: 0x17001E22 RID: 7714
		// (get) Token: 0x06002D1B RID: 11547 RVA: 0x0008A3C0 File Offset: 0x000885C0
		// (set) Token: 0x06002D1C RID: 11548 RVA: 0x0008A3E9 File Offset: 0x000885E9
		public SwitchParameter Preview
		{
			get
			{
				object obj = base["Preview"];
				if (obj != null)
				{
					return (SwitchParameter)obj;
				}
				return new SwitchParameter(false);
			}
			set
			{
				base["Preview"] = value;
			}
		}

		// Token: 0x17001E23 RID: 7715
		// (get) Token: 0x06002D1D RID: 11549 RVA: 0x0008A3FC File Offset: 0x000885FC
		// (set) Token: 0x06002D1E RID: 11550 RVA: 0x0008A425 File Offset: 0x00088625
		public SwitchParameter Save
		{
			get
			{
				object obj = base["Save"];
				if (obj != null)
				{
					return (SwitchParameter)obj;
				}
				return new SwitchParameter(false);
			}
			set
			{
				base["Save"] = value;
			}
		}

		// Token: 0x17001E24 RID: 7716
		// (get) Token: 0x06002D1F RID: 11551 RVA: 0x0008A438 File Offset: 0x00088638
		// (set) Token: 0x06002D20 RID: 11552 RVA: 0x0008A461 File Offset: 0x00088661
		public SwitchParameter Cancel
		{
			get
			{
				object obj = base["Cancel"];
				if (obj != null)
				{
					return (SwitchParameter)obj;
				}
				return new SwitchParameter(false);
			}
			set
			{
				base["Cancel"] = value;
			}
		}

		// Token: 0x17001E25 RID: 7717
		// (get) Token: 0x06002D21 RID: 11553 RVA: 0x0008A474 File Offset: 0x00088674
		public override string AssociatedCmdlet
		{
			get
			{
				return "Set-UserPhoto";
			}
		}

		// Token: 0x17001E26 RID: 7718
		// (get) Token: 0x06002D22 RID: 11554 RVA: 0x0008A47B File Offset: 0x0008867B
		public override string RbacScope
		{
			get
			{
				return "@W:Self";
			}
		}
	}
}
