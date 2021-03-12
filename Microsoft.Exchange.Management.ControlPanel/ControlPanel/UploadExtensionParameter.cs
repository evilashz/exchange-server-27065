using System;
using System.IO;
using System.Management.Automation;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020001D8 RID: 472
	[DataContract]
	public class UploadExtensionParameter : WebServiceParameters
	{
		// Token: 0x17001B95 RID: 7061
		// (get) Token: 0x06002585 RID: 9605 RVA: 0x00073540 File Offset: 0x00071740
		public override string AssociatedCmdlet
		{
			get
			{
				return "New-App";
			}
		}

		// Token: 0x17001B96 RID: 7062
		// (get) Token: 0x06002586 RID: 9606 RVA: 0x00073547 File Offset: 0x00071747
		public override string RbacScope
		{
			get
			{
				return "@W:Self";
			}
		}

		// Token: 0x17001B97 RID: 7063
		// (get) Token: 0x06002587 RID: 9607 RVA: 0x0007354E File Offset: 0x0007174E
		// (set) Token: 0x06002588 RID: 9608 RVA: 0x00073560 File Offset: 0x00071760
		public Stream FileStream
		{
			get
			{
				return (Stream)base["FileStream"];
			}
			set
			{
				base["FileStream"] = value;
			}
		}

		// Token: 0x17001B98 RID: 7064
		// (get) Token: 0x06002589 RID: 9609 RVA: 0x00073570 File Offset: 0x00071770
		// (set) Token: 0x0600258A RID: 9610 RVA: 0x00073599 File Offset: 0x00071799
		public SwitchParameter AllowReadWriteMailbox
		{
			get
			{
				object obj = base["AllowReadWriteMailbox"];
				if (obj != null)
				{
					return (SwitchParameter)obj;
				}
				return new SwitchParameter(false);
			}
			set
			{
				base["AllowReadWriteMailbox"] = value;
			}
		}
	}
}
