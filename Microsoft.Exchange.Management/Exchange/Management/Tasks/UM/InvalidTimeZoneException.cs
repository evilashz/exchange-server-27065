using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks.UM
{
	// Token: 0x020011DF RID: 4575
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class InvalidTimeZoneException : LocalizedException
	{
		// Token: 0x0600B93A RID: 47418 RVA: 0x002A5A36 File Offset: 0x002A3C36
		public InvalidTimeZoneException(string tzKeyName) : base(Strings.InvalidTimeZone(tzKeyName))
		{
			this.tzKeyName = tzKeyName;
		}

		// Token: 0x0600B93B RID: 47419 RVA: 0x002A5A4B File Offset: 0x002A3C4B
		public InvalidTimeZoneException(string tzKeyName, Exception innerException) : base(Strings.InvalidTimeZone(tzKeyName), innerException)
		{
			this.tzKeyName = tzKeyName;
		}

		// Token: 0x0600B93C RID: 47420 RVA: 0x002A5A61 File Offset: 0x002A3C61
		protected InvalidTimeZoneException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.tzKeyName = (string)info.GetValue("tzKeyName", typeof(string));
		}

		// Token: 0x0600B93D RID: 47421 RVA: 0x002A5A8B File Offset: 0x002A3C8B
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("tzKeyName", this.tzKeyName);
		}

		// Token: 0x17003A3B RID: 14907
		// (get) Token: 0x0600B93E RID: 47422 RVA: 0x002A5AA6 File Offset: 0x002A3CA6
		public string TzKeyName
		{
			get
			{
				return this.tzKeyName;
			}
		}

		// Token: 0x04006456 RID: 25686
		private readonly string tzKeyName;
	}
}
