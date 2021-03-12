using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000E83 RID: 3715
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class InvalidUMFaxServerURIValue : LocalizedException
	{
		// Token: 0x0600A761 RID: 42849 RVA: 0x002884B8 File Offset: 0x002866B8
		public InvalidUMFaxServerURIValue(string faxServerUri) : base(Strings.InvalidUMFaxServerURIValue(faxServerUri))
		{
			this.faxServerUri = faxServerUri;
		}

		// Token: 0x0600A762 RID: 42850 RVA: 0x002884CD File Offset: 0x002866CD
		public InvalidUMFaxServerURIValue(string faxServerUri, Exception innerException) : base(Strings.InvalidUMFaxServerURIValue(faxServerUri), innerException)
		{
			this.faxServerUri = faxServerUri;
		}

		// Token: 0x0600A763 RID: 42851 RVA: 0x002884E3 File Offset: 0x002866E3
		protected InvalidUMFaxServerURIValue(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.faxServerUri = (string)info.GetValue("faxServerUri", typeof(string));
		}

		// Token: 0x0600A764 RID: 42852 RVA: 0x0028850D File Offset: 0x0028670D
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("faxServerUri", this.faxServerUri);
		}

		// Token: 0x17003672 RID: 13938
		// (get) Token: 0x0600A765 RID: 42853 RVA: 0x00288528 File Offset: 0x00286728
		public string FaxServerUri
		{
			get
			{
				return this.faxServerUri;
			}
		}

		// Token: 0x04005FD8 RID: 24536
		private readonly string faxServerUri;
	}
}
