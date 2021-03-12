using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000DF5 RID: 3573
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class InvalidWKObjectTargetException : LocalizedException
	{
		// Token: 0x0600A4C2 RID: 42178 RVA: 0x00284D9C File Offset: 0x00282F9C
		public InvalidWKObjectTargetException(string guid, string container, string target, string groupType) : base(Strings.InvalidWKObjectTargetException(guid, container, target, groupType))
		{
			this.guid = guid;
			this.container = container;
			this.target = target;
			this.groupType = groupType;
		}

		// Token: 0x0600A4C3 RID: 42179 RVA: 0x00284DCB File Offset: 0x00282FCB
		public InvalidWKObjectTargetException(string guid, string container, string target, string groupType, Exception innerException) : base(Strings.InvalidWKObjectTargetException(guid, container, target, groupType), innerException)
		{
			this.guid = guid;
			this.container = container;
			this.target = target;
			this.groupType = groupType;
		}

		// Token: 0x0600A4C4 RID: 42180 RVA: 0x00284DFC File Offset: 0x00282FFC
		protected InvalidWKObjectTargetException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.guid = (string)info.GetValue("guid", typeof(string));
			this.container = (string)info.GetValue("container", typeof(string));
			this.target = (string)info.GetValue("target", typeof(string));
			this.groupType = (string)info.GetValue("groupType", typeof(string));
		}

		// Token: 0x0600A4C5 RID: 42181 RVA: 0x00284E94 File Offset: 0x00283094
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("guid", this.guid);
			info.AddValue("container", this.container);
			info.AddValue("target", this.target);
			info.AddValue("groupType", this.groupType);
		}

		// Token: 0x1700360B RID: 13835
		// (get) Token: 0x0600A4C6 RID: 42182 RVA: 0x00284EED File Offset: 0x002830ED
		public string Guid
		{
			get
			{
				return this.guid;
			}
		}

		// Token: 0x1700360C RID: 13836
		// (get) Token: 0x0600A4C7 RID: 42183 RVA: 0x00284EF5 File Offset: 0x002830F5
		public string Container
		{
			get
			{
				return this.container;
			}
		}

		// Token: 0x1700360D RID: 13837
		// (get) Token: 0x0600A4C8 RID: 42184 RVA: 0x00284EFD File Offset: 0x002830FD
		public string Target
		{
			get
			{
				return this.target;
			}
		}

		// Token: 0x1700360E RID: 13838
		// (get) Token: 0x0600A4C9 RID: 42185 RVA: 0x00284F05 File Offset: 0x00283105
		public string GroupType
		{
			get
			{
				return this.groupType;
			}
		}

		// Token: 0x04005F71 RID: 24433
		private readonly string guid;

		// Token: 0x04005F72 RID: 24434
		private readonly string container;

		// Token: 0x04005F73 RID: 24435
		private readonly string target;

		// Token: 0x04005F74 RID: 24436
		private readonly string groupType;
	}
}
