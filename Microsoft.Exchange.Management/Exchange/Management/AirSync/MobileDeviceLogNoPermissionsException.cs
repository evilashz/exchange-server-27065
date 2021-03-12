using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.AirSync
{
	// Token: 0x02000E2C RID: 3628
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class MobileDeviceLogNoPermissionsException : LocalizedException
	{
		// Token: 0x0600A5E2 RID: 42466 RVA: 0x00286C35 File Offset: 0x00284E35
		public MobileDeviceLogNoPermissionsException(string path) : base(Strings.MobileDeviceLogNoPermissionsException(path))
		{
			this.path = path;
		}

		// Token: 0x0600A5E3 RID: 42467 RVA: 0x00286C4A File Offset: 0x00284E4A
		public MobileDeviceLogNoPermissionsException(string path, Exception innerException) : base(Strings.MobileDeviceLogNoPermissionsException(path), innerException)
		{
			this.path = path;
		}

		// Token: 0x0600A5E4 RID: 42468 RVA: 0x00286C60 File Offset: 0x00284E60
		protected MobileDeviceLogNoPermissionsException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.path = (string)info.GetValue("path", typeof(string));
		}

		// Token: 0x0600A5E5 RID: 42469 RVA: 0x00286C8A File Offset: 0x00284E8A
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("path", this.path);
		}

		// Token: 0x1700364F RID: 13903
		// (get) Token: 0x0600A5E6 RID: 42470 RVA: 0x00286CA5 File Offset: 0x00284EA5
		public string Path
		{
			get
			{
				return this.path;
			}
		}

		// Token: 0x04005FB5 RID: 24501
		private readonly string path;
	}
}
