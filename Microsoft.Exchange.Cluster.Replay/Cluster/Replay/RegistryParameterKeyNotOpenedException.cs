using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x0200051B RID: 1307
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class RegistryParameterKeyNotOpenedException : RegistryParameterException
	{
		// Token: 0x06002F9E RID: 12190 RVA: 0x000C5E3D File Offset: 0x000C403D
		public RegistryParameterKeyNotOpenedException(string keyName) : base(ReplayStrings.RegistryParameterKeyNotOpenedException(keyName))
		{
			this.keyName = keyName;
		}

		// Token: 0x06002F9F RID: 12191 RVA: 0x000C5E57 File Offset: 0x000C4057
		public RegistryParameterKeyNotOpenedException(string keyName, Exception innerException) : base(ReplayStrings.RegistryParameterKeyNotOpenedException(keyName), innerException)
		{
			this.keyName = keyName;
		}

		// Token: 0x06002FA0 RID: 12192 RVA: 0x000C5E72 File Offset: 0x000C4072
		protected RegistryParameterKeyNotOpenedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.keyName = (string)info.GetValue("keyName", typeof(string));
		}

		// Token: 0x06002FA1 RID: 12193 RVA: 0x000C5E9C File Offset: 0x000C409C
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("keyName", this.keyName);
		}

		// Token: 0x17000C3C RID: 3132
		// (get) Token: 0x06002FA2 RID: 12194 RVA: 0x000C5EB7 File Offset: 0x000C40B7
		public string KeyName
		{
			get
			{
				return this.keyName;
			}
		}

		// Token: 0x040015D9 RID: 5593
		private readonly string keyName;
	}
}
