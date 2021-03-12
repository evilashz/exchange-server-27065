using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x02000416 RID: 1046
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class SetupVersionInformationCorruptException : LocalizedException
	{
		// Token: 0x06001946 RID: 6470 RVA: 0x0005F7B4 File Offset: 0x0005D9B4
		public SetupVersionInformationCorruptException(string keyPath) : base(DiagnosticsResources.ExceptionSetupVersionInformationCorrupt(keyPath))
		{
			this.keyPath = keyPath;
		}

		// Token: 0x06001947 RID: 6471 RVA: 0x0005F7C9 File Offset: 0x0005D9C9
		public SetupVersionInformationCorruptException(string keyPath, Exception innerException) : base(DiagnosticsResources.ExceptionSetupVersionInformationCorrupt(keyPath), innerException)
		{
			this.keyPath = keyPath;
		}

		// Token: 0x06001948 RID: 6472 RVA: 0x0005F7DF File Offset: 0x0005D9DF
		protected SetupVersionInformationCorruptException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.keyPath = (string)info.GetValue("keyPath", typeof(string));
		}

		// Token: 0x06001949 RID: 6473 RVA: 0x0005F809 File Offset: 0x0005DA09
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("keyPath", this.keyPath);
		}

		// Token: 0x17000AFE RID: 2814
		// (get) Token: 0x0600194A RID: 6474 RVA: 0x0005F824 File Offset: 0x0005DA24
		public string KeyPath
		{
			get
			{
				return this.keyPath;
			}
		}

		// Token: 0x04001DEA RID: 7658
		private readonly string keyPath;
	}
}
