using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Data.Mapi.Common
{
	// Token: 0x02000045 RID: 69
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class MapiCalculatedPropertyGettingException : MapiConvertingException
	{
		// Token: 0x06000280 RID: 640 RVA: 0x0000E13C File Offset: 0x0000C33C
		public MapiCalculatedPropertyGettingException(string name, string details) : base(Strings.MapiCalculatedPropertyGettingExceptionError(name, details))
		{
			this.name = name;
			this.details = details;
		}

		// Token: 0x06000281 RID: 641 RVA: 0x0000E159 File Offset: 0x0000C359
		public MapiCalculatedPropertyGettingException(string name, string details, Exception innerException) : base(Strings.MapiCalculatedPropertyGettingExceptionError(name, details), innerException)
		{
			this.name = name;
			this.details = details;
		}

		// Token: 0x06000282 RID: 642 RVA: 0x0000E178 File Offset: 0x0000C378
		protected MapiCalculatedPropertyGettingException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.name = (string)info.GetValue("name", typeof(string));
			this.details = (string)info.GetValue("details", typeof(string));
		}

		// Token: 0x06000283 RID: 643 RVA: 0x0000E1CD File Offset: 0x0000C3CD
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("name", this.name);
			info.AddValue("details", this.details);
		}

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x06000284 RID: 644 RVA: 0x0000E1F9 File Offset: 0x0000C3F9
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x06000285 RID: 645 RVA: 0x0000E201 File Offset: 0x0000C401
		public string Details
		{
			get
			{
				return this.details;
			}
		}

		// Token: 0x04000199 RID: 409
		private readonly string name;

		// Token: 0x0400019A RID: 410
		private readonly string details;
	}
}
