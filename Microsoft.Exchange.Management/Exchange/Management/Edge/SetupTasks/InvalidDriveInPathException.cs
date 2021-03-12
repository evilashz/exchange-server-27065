using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Edge.SetupTasks
{
	// Token: 0x02001226 RID: 4646
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class InvalidDriveInPathException : LocalizedException
	{
		// Token: 0x0600BB1E RID: 47902 RVA: 0x002A9CB6 File Offset: 0x002A7EB6
		public InvalidDriveInPathException(string path) : base(Strings.InvalidDriveInPath(path))
		{
			this.path = path;
		}

		// Token: 0x0600BB1F RID: 47903 RVA: 0x002A9CCB File Offset: 0x002A7ECB
		public InvalidDriveInPathException(string path, Exception innerException) : base(Strings.InvalidDriveInPath(path), innerException)
		{
			this.path = path;
		}

		// Token: 0x0600BB20 RID: 47904 RVA: 0x002A9CE1 File Offset: 0x002A7EE1
		protected InvalidDriveInPathException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.path = (string)info.GetValue("path", typeof(string));
		}

		// Token: 0x0600BB21 RID: 47905 RVA: 0x002A9D0B File Offset: 0x002A7F0B
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("path", this.path);
		}

		// Token: 0x17003AE6 RID: 15078
		// (get) Token: 0x0600BB22 RID: 47906 RVA: 0x002A9D26 File Offset: 0x002A7F26
		public string Path
		{
			get
			{
				return this.path;
			}
		}

		// Token: 0x0400656E RID: 25966
		private readonly string path;
	}
}
