using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Shared
{
	// Token: 0x020000E4 RID: 228
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class RegistryParameterKeyNotOpenedException : RegistryParameterException
	{
		// Token: 0x060007AF RID: 1967 RVA: 0x0001C985 File Offset: 0x0001AB85
		public RegistryParameterKeyNotOpenedException(string keyName) : base(Strings.RegistryParameterKeyNotOpenedException(keyName))
		{
			this.keyName = keyName;
		}

		// Token: 0x060007B0 RID: 1968 RVA: 0x0001C99F File Offset: 0x0001AB9F
		public RegistryParameterKeyNotOpenedException(string keyName, Exception innerException) : base(Strings.RegistryParameterKeyNotOpenedException(keyName), innerException)
		{
			this.keyName = keyName;
		}

		// Token: 0x060007B1 RID: 1969 RVA: 0x0001C9BA File Offset: 0x0001ABBA
		protected RegistryParameterKeyNotOpenedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.keyName = (string)info.GetValue("keyName", typeof(string));
		}

		// Token: 0x060007B2 RID: 1970 RVA: 0x0001C9E4 File Offset: 0x0001ABE4
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("keyName", this.keyName);
		}

		// Token: 0x1700025F RID: 607
		// (get) Token: 0x060007B3 RID: 1971 RVA: 0x0001C9FF File Offset: 0x0001ABFF
		public string KeyName
		{
			get
			{
				return this.keyName;
			}
		}

		// Token: 0x0400073F RID: 1855
		private readonly string keyName;
	}
}
