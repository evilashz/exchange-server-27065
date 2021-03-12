using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Configuration.Common.LocStrings;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x020002D1 RID: 721
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class DuplicateExternalDirectoryObjectIdException : LocalizedException
	{
		// Token: 0x0600197E RID: 6526 RVA: 0x0005D38B File Offset: 0x0005B58B
		public DuplicateExternalDirectoryObjectIdException(string objectName, string edoId) : base(Strings.DuplicateExternalDirectoryObjectIdException(objectName, edoId))
		{
			this.objectName = objectName;
			this.edoId = edoId;
		}

		// Token: 0x0600197F RID: 6527 RVA: 0x0005D3A8 File Offset: 0x0005B5A8
		public DuplicateExternalDirectoryObjectIdException(string objectName, string edoId, Exception innerException) : base(Strings.DuplicateExternalDirectoryObjectIdException(objectName, edoId), innerException)
		{
			this.objectName = objectName;
			this.edoId = edoId;
		}

		// Token: 0x06001980 RID: 6528 RVA: 0x0005D3C8 File Offset: 0x0005B5C8
		protected DuplicateExternalDirectoryObjectIdException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.objectName = (string)info.GetValue("objectName", typeof(string));
			this.edoId = (string)info.GetValue("edoId", typeof(string));
		}

		// Token: 0x06001981 RID: 6529 RVA: 0x0005D41D File Offset: 0x0005B61D
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("objectName", this.objectName);
			info.AddValue("edoId", this.edoId);
		}

		// Token: 0x170004CC RID: 1228
		// (get) Token: 0x06001982 RID: 6530 RVA: 0x0005D449 File Offset: 0x0005B649
		public string ObjectName
		{
			get
			{
				return this.objectName;
			}
		}

		// Token: 0x170004CD RID: 1229
		// (get) Token: 0x06001983 RID: 6531 RVA: 0x0005D451 File Offset: 0x0005B651
		public string EdoId
		{
			get
			{
				return this.edoId;
			}
		}

		// Token: 0x0400099F RID: 2463
		private readonly string objectName;

		// Token: 0x040009A0 RID: 2464
		private readonly string edoId;
	}
}
