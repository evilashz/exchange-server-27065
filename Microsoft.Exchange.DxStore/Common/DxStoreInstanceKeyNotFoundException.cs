using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.DxStore.Common
{
	// Token: 0x020000A8 RID: 168
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class DxStoreInstanceKeyNotFoundException : DxStoreInstanceServerException
	{
		// Token: 0x06000608 RID: 1544 RVA: 0x00014850 File Offset: 0x00012A50
		public DxStoreInstanceKeyNotFoundException(string keyName) : base(Strings.DxStoreInstanceKeyNotFound(keyName))
		{
			this.keyName = keyName;
		}

		// Token: 0x06000609 RID: 1545 RVA: 0x0001486A File Offset: 0x00012A6A
		public DxStoreInstanceKeyNotFoundException(string keyName, Exception innerException) : base(Strings.DxStoreInstanceKeyNotFound(keyName), innerException)
		{
			this.keyName = keyName;
		}

		// Token: 0x0600060A RID: 1546 RVA: 0x00014885 File Offset: 0x00012A85
		protected DxStoreInstanceKeyNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.keyName = (string)info.GetValue("keyName", typeof(string));
		}

		// Token: 0x0600060B RID: 1547 RVA: 0x000148AF File Offset: 0x00012AAF
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("keyName", this.keyName);
		}

		// Token: 0x17000185 RID: 389
		// (get) Token: 0x0600060C RID: 1548 RVA: 0x000148CA File Offset: 0x00012ACA
		public string KeyName
		{
			get
			{
				return this.keyName;
			}
		}

		// Token: 0x04000296 RID: 662
		private readonly string keyName;
	}
}
