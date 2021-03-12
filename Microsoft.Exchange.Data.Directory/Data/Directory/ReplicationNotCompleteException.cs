using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x02000AC8 RID: 2760
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ReplicationNotCompleteException : ADOperationException
	{
		// Token: 0x060080AA RID: 32938 RVA: 0x001A5A83 File Offset: 0x001A3C83
		public ReplicationNotCompleteException(string forestName, string dcName) : base(DirectoryStrings.ReplicationNotComplete(forestName, dcName))
		{
			this.forestName = forestName;
			this.dcName = dcName;
		}

		// Token: 0x060080AB RID: 32939 RVA: 0x001A5AA0 File Offset: 0x001A3CA0
		public ReplicationNotCompleteException(string forestName, string dcName, Exception innerException) : base(DirectoryStrings.ReplicationNotComplete(forestName, dcName), innerException)
		{
			this.forestName = forestName;
			this.dcName = dcName;
		}

		// Token: 0x060080AC RID: 32940 RVA: 0x001A5AC0 File Offset: 0x001A3CC0
		protected ReplicationNotCompleteException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.forestName = (string)info.GetValue("forestName", typeof(string));
			this.dcName = (string)info.GetValue("dcName", typeof(string));
		}

		// Token: 0x060080AD RID: 32941 RVA: 0x001A5B15 File Offset: 0x001A3D15
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("forestName", this.forestName);
			info.AddValue("dcName", this.dcName);
		}

		// Token: 0x17002EE1 RID: 12001
		// (get) Token: 0x060080AE RID: 32942 RVA: 0x001A5B41 File Offset: 0x001A3D41
		public string ForestName
		{
			get
			{
				return this.forestName;
			}
		}

		// Token: 0x17002EE2 RID: 12002
		// (get) Token: 0x060080AF RID: 32943 RVA: 0x001A5B49 File Offset: 0x001A3D49
		public string DcName
		{
			get
			{
				return this.dcName;
			}
		}

		// Token: 0x040055BB RID: 21947
		private readonly string forestName;

		// Token: 0x040055BC RID: 21948
		private readonly string dcName;
	}
}
