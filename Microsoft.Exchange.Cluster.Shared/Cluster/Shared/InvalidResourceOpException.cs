using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Shared
{
	// Token: 0x020000C9 RID: 201
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class InvalidResourceOpException : ClusCommonFailException
	{
		// Token: 0x06000721 RID: 1825 RVA: 0x0001B9A0 File Offset: 0x00019BA0
		public InvalidResourceOpException(string resName) : base(Strings.InvalidResourceOpException(resName))
		{
			this.resName = resName;
		}

		// Token: 0x06000722 RID: 1826 RVA: 0x0001B9BA File Offset: 0x00019BBA
		public InvalidResourceOpException(string resName, Exception innerException) : base(Strings.InvalidResourceOpException(resName), innerException)
		{
			this.resName = resName;
		}

		// Token: 0x06000723 RID: 1827 RVA: 0x0001B9D5 File Offset: 0x00019BD5
		protected InvalidResourceOpException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.resName = (string)info.GetValue("resName", typeof(string));
		}

		// Token: 0x06000724 RID: 1828 RVA: 0x0001B9FF File Offset: 0x00019BFF
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("resName", this.resName);
		}

		// Token: 0x1700023D RID: 573
		// (get) Token: 0x06000725 RID: 1829 RVA: 0x0001BA1A File Offset: 0x00019C1A
		public string ResName
		{
			get
			{
				return this.resName;
			}
		}

		// Token: 0x0400071D RID: 1821
		private readonly string resName;
	}
}
