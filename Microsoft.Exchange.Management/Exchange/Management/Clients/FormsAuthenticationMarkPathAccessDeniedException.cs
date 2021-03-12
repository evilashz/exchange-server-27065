using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.Clients
{
	// Token: 0x02000DE6 RID: 3558
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class FormsAuthenticationMarkPathAccessDeniedException : DataSourceOperationException
	{
		// Token: 0x0600A46E RID: 42094 RVA: 0x00284382 File Offset: 0x00282582
		public FormsAuthenticationMarkPathAccessDeniedException(string metabasePath, int propertyID) : base(Strings.FormsAuthenticationMarkPathAccessDeniedException(metabasePath, propertyID))
		{
			this.metabasePath = metabasePath;
			this.propertyID = propertyID;
		}

		// Token: 0x0600A46F RID: 42095 RVA: 0x0028439F File Offset: 0x0028259F
		public FormsAuthenticationMarkPathAccessDeniedException(string metabasePath, int propertyID, Exception innerException) : base(Strings.FormsAuthenticationMarkPathAccessDeniedException(metabasePath, propertyID), innerException)
		{
			this.metabasePath = metabasePath;
			this.propertyID = propertyID;
		}

		// Token: 0x0600A470 RID: 42096 RVA: 0x002843C0 File Offset: 0x002825C0
		protected FormsAuthenticationMarkPathAccessDeniedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.metabasePath = (string)info.GetValue("metabasePath", typeof(string));
			this.propertyID = (int)info.GetValue("propertyID", typeof(int));
		}

		// Token: 0x0600A471 RID: 42097 RVA: 0x00284415 File Offset: 0x00282615
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("metabasePath", this.metabasePath);
			info.AddValue("propertyID", this.propertyID);
		}

		// Token: 0x170035F3 RID: 13811
		// (get) Token: 0x0600A472 RID: 42098 RVA: 0x00284441 File Offset: 0x00282641
		public string MetabasePath
		{
			get
			{
				return this.metabasePath;
			}
		}

		// Token: 0x170035F4 RID: 13812
		// (get) Token: 0x0600A473 RID: 42099 RVA: 0x00284449 File Offset: 0x00282649
		public int PropertyID
		{
			get
			{
				return this.propertyID;
			}
		}

		// Token: 0x04005F59 RID: 24409
		private readonly string metabasePath;

		// Token: 0x04005F5A RID: 24410
		private readonly int propertyID;
	}
}
