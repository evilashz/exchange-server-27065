using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;
using Microsoft.Exchange.Net;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020004D7 RID: 1239
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class ContactsExporter
	{
		// Token: 0x06003611 RID: 13841 RVA: 0x000DA04C File Offset: 0x000D824C
		public ContactsExporter(PropertyDefinition[] properties, IEnumerable<IStorePropertyBag> contactsEnumerator)
		{
			Util.ThrowOnNullArgument(properties, "properties");
			Util.ThrowOnNullArgument(contactsEnumerator, "contactsEnumerator");
			Util.ThrowOnArgumentOutOfRangeOnLessThan(properties.Length, 0, "properties");
			this.properties = properties;
			this.headers = properties.Select(new Func<PropertyDefinition, string>(ContactsExporter.OutlookCsvExportHelper.GetPropertyTitle));
			this.contactsEnumerator = contactsEnumerator;
		}

		// Token: 0x170010CE RID: 4302
		// (get) Token: 0x06003612 RID: 13842 RVA: 0x000DA0A9 File Offset: 0x000D82A9
		public string Format
		{
			get
			{
				return "outlook_csv";
			}
		}

		// Token: 0x170010CF RID: 4303
		// (get) Token: 0x06003613 RID: 13843 RVA: 0x000DA0B0 File Offset: 0x000D82B0
		public string ContentType
		{
			get
			{
				return "text/plain; charset=utf-8";
			}
		}

		// Token: 0x06003614 RID: 13844 RVA: 0x000DA0B8 File Offset: 0x000D82B8
		public Stream GetStreamFromContacts()
		{
			ContactsExporter.Tracer.TraceFunction((long)this.GetHashCode(), "Entering ContactExporter.GetStreamFromContacts.");
			Stream stream = new PooledMemoryStream(1024);
			Utf8Csv.WriteBom(stream);
			Utf8Csv.WriteHeaderRow(stream, this.headers.ToArray<string>());
			this.WriteAllContacts(stream);
			stream.Position = 0L;
			ContactsExporter.Tracer.TraceFunction<long>((long)this.GetHashCode(), "Leaving ContactExporter.GetStreamFromContacts. Data Size {0}", stream.Length);
			return stream;
		}

		// Token: 0x06003615 RID: 13845 RVA: 0x000DA12C File Offset: 0x000D832C
		private void WriteAllContacts(Stream stream)
		{
			foreach (IStorePropertyBag contact in this.contactsEnumerator)
			{
				this.WriteContact(stream, contact);
			}
		}

		// Token: 0x06003616 RID: 13846 RVA: 0x000DA17C File Offset: 0x000D837C
		private void WriteContact(Stream stream, IStorePropertyBag contact)
		{
			List<byte[]> list = new List<byte[]>(this.properties.Length);
			foreach (PropertyDefinition propertyDefinition in this.properties)
			{
				object obj = contact.TryGetProperty(propertyDefinition);
				if (obj == null || PropertyError.IsPropertyNotFound(obj))
				{
					obj = string.Empty;
				}
				list.Add(Utf8Csv.EncodeAndEscape(obj.ToString()));
			}
			Utf8Csv.WriteRawRow(stream, list.ToArray());
		}

		// Token: 0x04001D04 RID: 7428
		private const int InitialBufferSize = 1024;

		// Token: 0x04001D05 RID: 7429
		private const string ContactsExporterDataFormat = "outlook_csv";

		// Token: 0x04001D06 RID: 7430
		private const string CsvFileContentType = "text/plain; charset=utf-8";

		// Token: 0x04001D07 RID: 7431
		protected static readonly Trace Tracer = ExTraceGlobals.ContactExporterTracer;

		// Token: 0x04001D08 RID: 7432
		private readonly PropertyDefinition[] properties;

		// Token: 0x04001D09 RID: 7433
		private readonly IEnumerable<string> headers;

		// Token: 0x04001D0A RID: 7434
		private readonly IEnumerable<IStorePropertyBag> contactsEnumerator;

		// Token: 0x020004D8 RID: 1240
		internal class OutlookCsvExportHelper
		{
			// Token: 0x06003618 RID: 13848 RVA: 0x000DA1F8 File Offset: 0x000D83F8
			public static string GetPropertyTitle(PropertyDefinition property)
			{
				string name;
				if (!ContactsExporter.OutlookCsvExportHelper.ColumnMapping.TryGetValue(property, out name))
				{
					name = property.Name;
				}
				return name;
			}

			// Token: 0x04001D0B RID: 7435
			private static readonly Dictionary<PropertyDefinition, string> ColumnMapping = new Dictionary<PropertyDefinition, string>
			{
				{
					ContactSchema.DisplayNamePrefix,
					"Title"
				},
				{
					ContactSchema.GivenName,
					"First Name"
				},
				{
					ContactSchema.MiddleName,
					"Middle Name"
				},
				{
					ContactSchema.Surname,
					"Last Name"
				},
				{
					ContactSchema.Generation,
					"Suffix"
				},
				{
					ContactSchema.Title,
					"Job Title"
				},
				{
					ContactSchema.Nickname,
					"Nickname"
				},
				{
					ContactSchema.CompanyName,
					"Company"
				},
				{
					ContactSchema.Department,
					"Department"
				},
				{
					ContactSchema.WorkAddressStreet,
					"Business Street"
				},
				{
					ContactSchema.WorkAddressCity,
					"Business City"
				},
				{
					ContactSchema.WorkAddressState,
					"Business State"
				},
				{
					ContactSchema.WorkAddressPostalCode,
					"Business Postal Code"
				},
				{
					ContactSchema.WorkAddressCountry,
					"Business Country/Region"
				},
				{
					ContactSchema.HomeStreet,
					"Home Street"
				},
				{
					ContactSchema.HomeCity,
					"Home City"
				},
				{
					ContactSchema.HomeState,
					"Home State"
				},
				{
					ContactSchema.HomePostalCode,
					"Home Postal Code"
				},
				{
					ContactSchema.HomeCountry,
					"Home Country/Region"
				},
				{
					ContactSchema.OtherStreet,
					"Other Street"
				},
				{
					ContactSchema.OtherCity,
					"Other City"
				},
				{
					ContactSchema.OtherState,
					"Other State"
				},
				{
					ContactSchema.OtherPostalCode,
					"Other Postal Code"
				},
				{
					ContactSchema.OtherCountry,
					"Other Country/Region"
				},
				{
					ContactSchema.AssistantPhoneNumber,
					"Assistant's Phone"
				},
				{
					ContactSchema.WorkFax,
					"Business Fax"
				},
				{
					ContactSchema.BusinessPhoneNumber,
					"Business Phone"
				},
				{
					ContactSchema.BusinessPhoneNumber2,
					"Business Phone 2"
				},
				{
					ContactSchema.CallbackPhone,
					"Callback"
				},
				{
					ContactSchema.CarPhone,
					"Car Phone"
				},
				{
					ContactSchema.OrganizationMainPhone,
					"Company Main Phone"
				},
				{
					ContactSchema.HomeFax,
					"Home Fax"
				},
				{
					ContactSchema.HomePhone,
					"Home Phone"
				},
				{
					ContactSchema.HomePhone2,
					"Home Phone 2"
				},
				{
					ContactSchema.InternationalIsdnNumber,
					"ISDN"
				},
				{
					ContactSchema.MobilePhone,
					"Mobile Phone"
				},
				{
					ContactSchema.OtherFax,
					"Other Fax"
				},
				{
					ContactSchema.OtherTelephone,
					"Other Phone"
				},
				{
					ContactSchema.Pager,
					"Pager"
				},
				{
					ContactSchema.PrimaryTelephoneNumber,
					"Primary Phone"
				},
				{
					ContactSchema.RadioPhone,
					"Radio Phone"
				},
				{
					ContactSchema.TtyTddPhoneNumber,
					"TTY/TDD Phone"
				},
				{
					ContactSchema.TelexNumber,
					"Telex"
				},
				{
					ContactSchema.WeddingAnniversary,
					"Anniversary"
				},
				{
					ContactSchema.AssistantName,
					"Assistant's Name"
				},
				{
					ContactSchema.Birthday,
					"Birthday"
				},
				{
					ContactSchema.WorkPostOfficeBox,
					"Business Address PO Box"
				},
				{
					ItemSchema.Categories,
					"Categories"
				},
				{
					ContactSchema.Children,
					"Children"
				},
				{
					ContactSchema.Email1EmailAddress,
					"E-mail Address"
				},
				{
					ContactSchema.Email2EmailAddress,
					"E-mail 2 Address"
				},
				{
					ContactSchema.Email3EmailAddress,
					"E-mail 3 Address"
				},
				{
					ContactSchema.GovernmentIdNumber,
					"Government ID Number"
				},
				{
					ContactSchema.Hobbies,
					"Hobby"
				},
				{
					ContactSchema.HomePostOfficeBox,
					"Home Address PO Box"
				},
				{
					ContactSchema.Initials,
					"Initials"
				},
				{
					ContactSchema.FreeBusyUrl,
					"Internet Free Busy"
				},
				{
					ContactSchema.Manager,
					"Manager's Name"
				},
				{
					ContactSchema.Mileage,
					"Mileage"
				},
				{
					ContactSchema.OfficeLocation,
					"Office Location"
				},
				{
					ContactSchema.OtherPostOfficeBox,
					"Other Address PO Box"
				},
				{
					ContactSchema.Profession,
					"Profession"
				},
				{
					ContactSchema.SpouseName,
					"Spouse"
				},
				{
					ContactSchema.UserText1,
					"User 1"
				},
				{
					ContactSchema.UserText2,
					"User 2"
				},
				{
					ContactSchema.UserText3,
					"User 3"
				},
				{
					ContactSchema.UserText4,
					"User 4"
				},
				{
					ContactSchema.BusinessHomePage,
					"Web Page"
				},
				{
					ContactSchema.YomiCompany,
					"Company Yomi"
				},
				{
					ContactSchema.YomiFirstName,
					"Given Yomi"
				},
				{
					ContactSchema.YomiLastName,
					"Surname Yomi"
				},
				{
					ContactSchema.Account,
					"Account"
				},
				{
					ContactSchema.BillingInformation,
					"Billing Information"
				},
				{
					ContactSchema.Location,
					"Location"
				}
			};
		}
	}
}
