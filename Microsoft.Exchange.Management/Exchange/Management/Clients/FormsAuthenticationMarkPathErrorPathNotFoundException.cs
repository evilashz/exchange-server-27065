using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.Clients
{
	// Token: 0x02000DE5 RID: 3557
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class FormsAuthenticationMarkPathErrorPathNotFoundException : DataSourceOperationException
	{
		// Token: 0x0600A469 RID: 42089 RVA: 0x0028430A File Offset: 0x0028250A
		public FormsAuthenticationMarkPathErrorPathNotFoundException(string metabasePath) : base(Strings.FormsAuthenticationMarkPathErrorPathNotFoundException(metabasePath))
		{
			this.metabasePath = metabasePath;
		}

		// Token: 0x0600A46A RID: 42090 RVA: 0x0028431F File Offset: 0x0028251F
		public FormsAuthenticationMarkPathErrorPathNotFoundException(string metabasePath, Exception innerException) : base(Strings.FormsAuthenticationMarkPathErrorPathNotFoundException(metabasePath), innerException)
		{
			this.metabasePath = metabasePath;
		}

		// Token: 0x0600A46B RID: 42091 RVA: 0x00284335 File Offset: 0x00282535
		protected FormsAuthenticationMarkPathErrorPathNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.metabasePath = (string)info.GetValue("metabasePath", typeof(string));
		}

		// Token: 0x0600A46C RID: 42092 RVA: 0x0028435F File Offset: 0x0028255F
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("metabasePath", this.metabasePath);
		}

		// Token: 0x170035F2 RID: 13810
		// (get) Token: 0x0600A46D RID: 42093 RVA: 0x0028437A File Offset: 0x0028257A
		public string MetabasePath
		{
			get
			{
				return this.metabasePath;
			}
		}

		// Token: 0x04005F58 RID: 24408
		private readonly string metabasePath;
	}
}
