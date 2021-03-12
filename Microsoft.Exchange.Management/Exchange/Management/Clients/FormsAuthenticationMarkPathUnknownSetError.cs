using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.Clients
{
	// Token: 0x02000DE8 RID: 3560
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class FormsAuthenticationMarkPathUnknownSetError : DataSourceOperationException
	{
		// Token: 0x0600A47A RID: 42106 RVA: 0x0028451D File Offset: 0x0028271D
		public FormsAuthenticationMarkPathUnknownSetError(string metabasePath, int propertyID, int hresult) : base(Strings.FormsAuthenticationMarkPathUnknownSetError(metabasePath, propertyID, hresult))
		{
			this.metabasePath = metabasePath;
			this.propertyID = propertyID;
			this.hresult = hresult;
		}

		// Token: 0x0600A47B RID: 42107 RVA: 0x00284542 File Offset: 0x00282742
		public FormsAuthenticationMarkPathUnknownSetError(string metabasePath, int propertyID, int hresult, Exception innerException) : base(Strings.FormsAuthenticationMarkPathUnknownSetError(metabasePath, propertyID, hresult), innerException)
		{
			this.metabasePath = metabasePath;
			this.propertyID = propertyID;
			this.hresult = hresult;
		}

		// Token: 0x0600A47C RID: 42108 RVA: 0x0028456C File Offset: 0x0028276C
		protected FormsAuthenticationMarkPathUnknownSetError(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.metabasePath = (string)info.GetValue("metabasePath", typeof(string));
			this.propertyID = (int)info.GetValue("propertyID", typeof(int));
			this.hresult = (int)info.GetValue("hresult", typeof(int));
		}

		// Token: 0x0600A47D RID: 42109 RVA: 0x002845E1 File Offset: 0x002827E1
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("metabasePath", this.metabasePath);
			info.AddValue("propertyID", this.propertyID);
			info.AddValue("hresult", this.hresult);
		}

		// Token: 0x170035F7 RID: 13815
		// (get) Token: 0x0600A47E RID: 42110 RVA: 0x0028461E File Offset: 0x0028281E
		public string MetabasePath
		{
			get
			{
				return this.metabasePath;
			}
		}

		// Token: 0x170035F8 RID: 13816
		// (get) Token: 0x0600A47F RID: 42111 RVA: 0x00284626 File Offset: 0x00282826
		public int PropertyID
		{
			get
			{
				return this.propertyID;
			}
		}

		// Token: 0x170035F9 RID: 13817
		// (get) Token: 0x0600A480 RID: 42112 RVA: 0x0028462E File Offset: 0x0028282E
		public int Hresult
		{
			get
			{
				return this.hresult;
			}
		}

		// Token: 0x04005F5D RID: 24413
		private readonly string metabasePath;

		// Token: 0x04005F5E RID: 24414
		private readonly int propertyID;

		// Token: 0x04005F5F RID: 24415
		private readonly int hresult;
	}
}
