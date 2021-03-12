using System;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.OnlineMeetings.OutlookAddinAdapter
{
	// Token: 0x020000CC RID: 204
	[KnownType(typeof(Permissions))]
	[XmlType("Permissions")]
	[DataContract(Name = "Permissions")]
	public class Permissions
	{
		// Token: 0x170001B7 RID: 439
		// (get) Token: 0x060004EB RID: 1259 RVA: 0x0000C1D5 File Offset: 0x0000A3D5
		// (set) Token: 0x060004EC RID: 1260 RVA: 0x0000C1DD File Offset: 0x0000A3DD
		[DataMember(Name = "AdmissionType", EmitDefaultValue = true)]
		[XmlElement("AdmissionType")]
		public AdmissionType AdmissionType { get; set; }

		// Token: 0x060004ED RID: 1261 RVA: 0x0000C1E8 File Offset: 0x0000A3E8
		internal static Permissions ConvertFrom(AccessLevel accessLevel)
		{
			Permissions permissions = new Permissions();
			switch (accessLevel)
			{
			case AccessLevel.SameEnterprise:
				permissions.AdmissionType = AdmissionType.ucOpenAuthenticated;
				break;
			case AccessLevel.Locked:
				permissions.AdmissionType = AdmissionType.ucLocked;
				break;
			case AccessLevel.Invited:
				permissions.AdmissionType = AdmissionType.ucClosedAuthenticated;
				break;
			case AccessLevel.Everyone:
				permissions.AdmissionType = AdmissionType.ucAnonymous;
				break;
			default:
				throw new InvalidEnumArgumentException("Invalid value for AdmissionType");
			}
			return permissions;
		}
	}
}
