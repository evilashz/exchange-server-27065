using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.Clients
{
	// Token: 0x02000DE4 RID: 3556
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class FormsAuthenticationErrorPathBusyException : TaskTransientException
	{
		// Token: 0x0600A464 RID: 42084 RVA: 0x00284292 File Offset: 0x00282492
		public FormsAuthenticationErrorPathBusyException(string metabasePath) : base(Strings.FormsAuthenticationErrorPathBusyException(metabasePath))
		{
			this.metabasePath = metabasePath;
		}

		// Token: 0x0600A465 RID: 42085 RVA: 0x002842A7 File Offset: 0x002824A7
		public FormsAuthenticationErrorPathBusyException(string metabasePath, Exception innerException) : base(Strings.FormsAuthenticationErrorPathBusyException(metabasePath), innerException)
		{
			this.metabasePath = metabasePath;
		}

		// Token: 0x0600A466 RID: 42086 RVA: 0x002842BD File Offset: 0x002824BD
		protected FormsAuthenticationErrorPathBusyException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.metabasePath = (string)info.GetValue("metabasePath", typeof(string));
		}

		// Token: 0x0600A467 RID: 42087 RVA: 0x002842E7 File Offset: 0x002824E7
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("metabasePath", this.metabasePath);
		}

		// Token: 0x170035F1 RID: 13809
		// (get) Token: 0x0600A468 RID: 42088 RVA: 0x00284302 File Offset: 0x00282502
		public string MetabasePath
		{
			get
			{
				return this.metabasePath;
			}
		}

		// Token: 0x04005F57 RID: 24407
		private readonly string metabasePath;
	}
}
