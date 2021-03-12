using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02001183 RID: 4483
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class OverrideNotFoundException : LocalizedException
	{
		// Token: 0x0600B680 RID: 46720 RVA: 0x0029FFC1 File Offset: 0x0029E1C1
		public OverrideNotFoundException(string identity, string type, string propertyName) : base(Strings.OverrideNotFound(identity, type, propertyName))
		{
			this.identity = identity;
			this.type = type;
			this.propertyName = propertyName;
		}

		// Token: 0x0600B681 RID: 46721 RVA: 0x0029FFE6 File Offset: 0x0029E1E6
		public OverrideNotFoundException(string identity, string type, string propertyName, Exception innerException) : base(Strings.OverrideNotFound(identity, type, propertyName), innerException)
		{
			this.identity = identity;
			this.type = type;
			this.propertyName = propertyName;
		}

		// Token: 0x0600B682 RID: 46722 RVA: 0x002A0010 File Offset: 0x0029E210
		protected OverrideNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.identity = (string)info.GetValue("identity", typeof(string));
			this.type = (string)info.GetValue("type", typeof(string));
			this.propertyName = (string)info.GetValue("propertyName", typeof(string));
		}

		// Token: 0x0600B683 RID: 46723 RVA: 0x002A0085 File Offset: 0x0029E285
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("identity", this.identity);
			info.AddValue("type", this.type);
			info.AddValue("propertyName", this.propertyName);
		}

		// Token: 0x17003991 RID: 14737
		// (get) Token: 0x0600B684 RID: 46724 RVA: 0x002A00C2 File Offset: 0x0029E2C2
		public string Identity
		{
			get
			{
				return this.identity;
			}
		}

		// Token: 0x17003992 RID: 14738
		// (get) Token: 0x0600B685 RID: 46725 RVA: 0x002A00CA File Offset: 0x0029E2CA
		public string Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x17003993 RID: 14739
		// (get) Token: 0x0600B686 RID: 46726 RVA: 0x002A00D2 File Offset: 0x0029E2D2
		public string PropertyName
		{
			get
			{
				return this.propertyName;
			}
		}

		// Token: 0x040062F7 RID: 25335
		private readonly string identity;

		// Token: 0x040062F8 RID: 25336
		private readonly string type;

		// Token: 0x040062F9 RID: 25337
		private readonly string propertyName;
	}
}
