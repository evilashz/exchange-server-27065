using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.OnlineMeetings.OutlookAddinAdapter
{
	// Token: 0x020000CA RID: 202
	[XmlType("Participants")]
	[DataContract(Name = "Participants")]
	[KnownType(typeof(Participants))]
	public class Participants
	{
		// Token: 0x170001B5 RID: 437
		// (get) Token: 0x060004E4 RID: 1252 RVA: 0x0000C0C2 File Offset: 0x0000A2C2
		// (set) Token: 0x060004E5 RID: 1253 RVA: 0x0000C0CA File Offset: 0x0000A2CA
		[XmlArray("Attendees")]
		[XmlArrayItem("User")]
		[DataMember(Name = "Attendees", EmitDefaultValue = true)]
		public User[] Attendees { get; set; }

		// Token: 0x170001B6 RID: 438
		// (get) Token: 0x060004E6 RID: 1254 RVA: 0x0000C0D3 File Offset: 0x0000A2D3
		// (set) Token: 0x060004E7 RID: 1255 RVA: 0x0000C0DB File Offset: 0x0000A2DB
		[XmlArray("Presenters")]
		[XmlArrayItem("User")]
		[DataMember(Name = "Presenters", EmitDefaultValue = true)]
		public User[] Presenters { get; set; }

		// Token: 0x060004E8 RID: 1256 RVA: 0x0000C0E4 File Offset: 0x0000A2E4
		internal static Participants ConvertFrom(IEnumerable<string> attendees, IEnumerable<string> leaders)
		{
			Participants participants = new Participants();
			Collection<User> collection = new Collection<User>();
			foreach (string attendee in attendees)
			{
				collection.Add(Participants.CreateUserFromSmtpAddress(attendee));
			}
			participants.Attendees = collection.ToArray<User>();
			Collection<User> collection2 = new Collection<User>();
			foreach (string attendee2 in leaders)
			{
				collection2.Add(Participants.CreateUserFromSmtpAddress(attendee2));
			}
			participants.Presenters = collection2.ToArray<User>();
			return participants;
		}

		// Token: 0x060004E9 RID: 1257 RVA: 0x0000C1A4 File Offset: 0x0000A3A4
		private static User CreateUserFromSmtpAddress(string attendee)
		{
			return new User
			{
				Name = attendee,
				SmtpAddress = attendee,
				SipAddress = attendee
			};
		}
	}
}
