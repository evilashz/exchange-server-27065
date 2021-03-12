using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x0200048F RID: 1167
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ReplayConfigPropException : TransientException
	{
		// Token: 0x06002C78 RID: 11384 RVA: 0x000BF6BD File Offset: 0x000BD8BD
		public ReplayConfigPropException(string id, string propertyName) : base(ReplayStrings.ReplayConfigPropException(id, propertyName))
		{
			this.id = id;
			this.propertyName = propertyName;
		}

		// Token: 0x06002C79 RID: 11385 RVA: 0x000BF6DA File Offset: 0x000BD8DA
		public ReplayConfigPropException(string id, string propertyName, Exception innerException) : base(ReplayStrings.ReplayConfigPropException(id, propertyName), innerException)
		{
			this.id = id;
			this.propertyName = propertyName;
		}

		// Token: 0x06002C7A RID: 11386 RVA: 0x000BF6F8 File Offset: 0x000BD8F8
		protected ReplayConfigPropException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.id = (string)info.GetValue("id", typeof(string));
			this.propertyName = (string)info.GetValue("propertyName", typeof(string));
		}

		// Token: 0x06002C7B RID: 11387 RVA: 0x000BF74D File Offset: 0x000BD94D
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("id", this.id);
			info.AddValue("propertyName", this.propertyName);
		}

		// Token: 0x17000B46 RID: 2886
		// (get) Token: 0x06002C7C RID: 11388 RVA: 0x000BF779 File Offset: 0x000BD979
		public string Id
		{
			get
			{
				return this.id;
			}
		}

		// Token: 0x17000B47 RID: 2887
		// (get) Token: 0x06002C7D RID: 11389 RVA: 0x000BF781 File Offset: 0x000BD981
		public string PropertyName
		{
			get
			{
				return this.propertyName;
			}
		}

		// Token: 0x040014E3 RID: 5347
		private readonly string id;

		// Token: 0x040014E4 RID: 5348
		private readonly string propertyName;
	}
}
