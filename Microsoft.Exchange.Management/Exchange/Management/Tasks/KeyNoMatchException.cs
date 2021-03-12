using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x020010E2 RID: 4322
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class KeyNoMatchException : LocalizedException
	{
		// Token: 0x0600B357 RID: 45911 RVA: 0x0029B113 File Offset: 0x00299313
		public KeyNoMatchException(string name) : base(Strings.KeyNoMatch(name))
		{
			this.name = name;
		}

		// Token: 0x0600B358 RID: 45912 RVA: 0x0029B128 File Offset: 0x00299328
		public KeyNoMatchException(string name, Exception innerException) : base(Strings.KeyNoMatch(name), innerException)
		{
			this.name = name;
		}

		// Token: 0x0600B359 RID: 45913 RVA: 0x0029B13E File Offset: 0x0029933E
		protected KeyNoMatchException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.name = (string)info.GetValue("name", typeof(string));
		}

		// Token: 0x0600B35A RID: 45914 RVA: 0x0029B168 File Offset: 0x00299368
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("name", this.name);
		}

		// Token: 0x170038EC RID: 14572
		// (get) Token: 0x0600B35B RID: 45915 RVA: 0x0029B183 File Offset: 0x00299383
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x04006252 RID: 25170
		private readonly string name;
	}
}
