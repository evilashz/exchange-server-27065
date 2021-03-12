using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.Clients
{
	// Token: 0x02000DE7 RID: 3559
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class FormsAuthenticationMarkPathCannotMarkSecureAttributeException : DataSourceOperationException
	{
		// Token: 0x0600A474 RID: 42100 RVA: 0x00284451 File Offset: 0x00282651
		public FormsAuthenticationMarkPathCannotMarkSecureAttributeException(string metabasePath, int propertyID) : base(Strings.FormsAuthenticationMarkPathCannotMarkSecureAttributeException(metabasePath, propertyID))
		{
			this.metabasePath = metabasePath;
			this.propertyID = propertyID;
		}

		// Token: 0x0600A475 RID: 42101 RVA: 0x0028446E File Offset: 0x0028266E
		public FormsAuthenticationMarkPathCannotMarkSecureAttributeException(string metabasePath, int propertyID, Exception innerException) : base(Strings.FormsAuthenticationMarkPathCannotMarkSecureAttributeException(metabasePath, propertyID), innerException)
		{
			this.metabasePath = metabasePath;
			this.propertyID = propertyID;
		}

		// Token: 0x0600A476 RID: 42102 RVA: 0x0028448C File Offset: 0x0028268C
		protected FormsAuthenticationMarkPathCannotMarkSecureAttributeException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.metabasePath = (string)info.GetValue("metabasePath", typeof(string));
			this.propertyID = (int)info.GetValue("propertyID", typeof(int));
		}

		// Token: 0x0600A477 RID: 42103 RVA: 0x002844E1 File Offset: 0x002826E1
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("metabasePath", this.metabasePath);
			info.AddValue("propertyID", this.propertyID);
		}

		// Token: 0x170035F5 RID: 13813
		// (get) Token: 0x0600A478 RID: 42104 RVA: 0x0028450D File Offset: 0x0028270D
		public string MetabasePath
		{
			get
			{
				return this.metabasePath;
			}
		}

		// Token: 0x170035F6 RID: 13814
		// (get) Token: 0x0600A479 RID: 42105 RVA: 0x00284515 File Offset: 0x00282715
		public int PropertyID
		{
			get
			{
				return this.propertyID;
			}
		}

		// Token: 0x04005F5B RID: 24411
		private readonly string metabasePath;

		// Token: 0x04005F5C RID: 24412
		private readonly int propertyID;
	}
}
