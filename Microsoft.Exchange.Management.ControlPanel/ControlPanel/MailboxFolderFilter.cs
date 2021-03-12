using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200060C RID: 1548
	[DataContract]
	public class MailboxFolderFilter : WebServiceParameters
	{
		// Token: 0x0600450E RID: 17678 RVA: 0x000D0E44 File Offset: 0x000CF044
		public MailboxFolderFilter()
		{
			this.OnDeserializing(default(StreamingContext));
		}

		// Token: 0x0600450F RID: 17679 RVA: 0x000D0E66 File Offset: 0x000CF066
		[OnDeserializing]
		private void OnDeserializing(StreamingContext context)
		{
			base["Recurse"] = true;
			base["MailFolderOnly"] = true;
			base["ResultSize"] = int.MaxValue;
		}

		// Token: 0x170026A7 RID: 9895
		// (get) Token: 0x06004510 RID: 17680 RVA: 0x000D0E9F File Offset: 0x000CF09F
		// (set) Token: 0x06004511 RID: 17681 RVA: 0x000D0EA7 File Offset: 0x000CF0A7
		[DataMember]
		public FolderPickerType FolderPickerType { get; set; }

		// Token: 0x170026A8 RID: 9896
		// (get) Token: 0x06004512 RID: 17682 RVA: 0x000D0EB0 File Offset: 0x000CF0B0
		public override string AssociatedCmdlet
		{
			get
			{
				return "Get-MailboxFolder";
			}
		}

		// Token: 0x170026A9 RID: 9897
		// (get) Token: 0x06004513 RID: 17683 RVA: 0x000D0EB7 File Offset: 0x000CF0B7
		public override string RbacScope
		{
			get
			{
				return "@R:Self";
			}
		}

		// Token: 0x04002E54 RID: 11860
		public const string RbacParameters = "?Recurse&MailFolderOnly&ResultSize";
	}
}
