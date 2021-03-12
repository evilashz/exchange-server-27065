using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x020001A5 RID: 421
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class ConfigKeyAccessRuntimeException : AnchorRuntimeException
	{
		// Token: 0x0600178E RID: 6030 RVA: 0x00070E3D File Offset: 0x0006F03D
		public ConfigKeyAccessRuntimeException(string keyname) : base(Strings.ConfigKeyAccessRuntimeError(keyname))
		{
			this.keyname = keyname;
		}

		// Token: 0x0600178F RID: 6031 RVA: 0x00070E52 File Offset: 0x0006F052
		public ConfigKeyAccessRuntimeException(string keyname, Exception innerException) : base(Strings.ConfigKeyAccessRuntimeError(keyname), innerException)
		{
			this.keyname = keyname;
		}

		// Token: 0x06001790 RID: 6032 RVA: 0x00070E68 File Offset: 0x0006F068
		protected ConfigKeyAccessRuntimeException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.keyname = (string)info.GetValue("keyname", typeof(string));
		}

		// Token: 0x06001791 RID: 6033 RVA: 0x00070E92 File Offset: 0x0006F092
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("keyname", this.keyname);
		}

		// Token: 0x1700077F RID: 1919
		// (get) Token: 0x06001792 RID: 6034 RVA: 0x00070EAD File Offset: 0x0006F0AD
		public string Keyname
		{
			get
			{
				return this.keyname;
			}
		}

		// Token: 0x04000B21 RID: 2849
		private readonly string keyname;
	}
}
