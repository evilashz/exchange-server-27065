using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Edge.SetupTasks
{
	// Token: 0x0200122B RID: 4651
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class InvalidPortNumberException : LocalizedException
	{
		// Token: 0x0600BB37 RID: 47927 RVA: 0x002A9F0E File Offset: 0x002A810E
		public InvalidPortNumberException(int port) : base(Strings.InvalidPortNumber(port))
		{
			this.port = port;
		}

		// Token: 0x0600BB38 RID: 47928 RVA: 0x002A9F23 File Offset: 0x002A8123
		public InvalidPortNumberException(int port, Exception innerException) : base(Strings.InvalidPortNumber(port), innerException)
		{
			this.port = port;
		}

		// Token: 0x0600BB39 RID: 47929 RVA: 0x002A9F39 File Offset: 0x002A8139
		protected InvalidPortNumberException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.port = (int)info.GetValue("port", typeof(int));
		}

		// Token: 0x0600BB3A RID: 47930 RVA: 0x002A9F63 File Offset: 0x002A8163
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("port", this.port);
		}

		// Token: 0x17003AEB RID: 15083
		// (get) Token: 0x0600BB3B RID: 47931 RVA: 0x002A9F7E File Offset: 0x002A817E
		public int Port
		{
			get
			{
				return this.port;
			}
		}

		// Token: 0x04006573 RID: 25971
		private readonly int port;
	}
}
