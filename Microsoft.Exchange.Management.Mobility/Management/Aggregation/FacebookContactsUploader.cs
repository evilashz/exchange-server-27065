using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.ApplicationLogic;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Net;
using Microsoft.Exchange.Management.Mobility;
using Microsoft.Exchange.Net.Facebook;

namespace Microsoft.Exchange.Management.Aggregation
{
	// Token: 0x02000019 RID: 25
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class FacebookContactsUploader
	{
		// Token: 0x060000E9 RID: 233 RVA: 0x00006868 File Offset: 0x00004A68
		internal FacebookContactsUploader(IContactsUploaderPerformanceTracker performanceTracker, IFacebookClient client, IPeopleConnectApplicationConfig configuration, Func<PropertyDefinition[], IEnumerable<IStorePropertyBag>> contactsEnumeratorBuilder)
		{
			ArgumentValidator.ThrowIfNull("performanceTracker", performanceTracker);
			ArgumentValidator.ThrowIfNull("client", client);
			ArgumentValidator.ThrowIfNull("configuration", configuration);
			ArgumentValidator.ThrowIfNull("contactsEnumerator", contactsEnumeratorBuilder);
			this.performanceTracker = performanceTracker;
			this.client = client;
			this.configuration = configuration;
			PropertyDefinition[] arg = PropertyDefinitionCollection.Merge<PropertyDefinition>(FacebookContactsUploader.ContactPropertiesToExport, FacebookContactsUploader.AdditionalContactPropertiesToLoad);
			this.contactsEnumerator = contactsEnumeratorBuilder(arg);
		}

		// Token: 0x060000EA RID: 234 RVA: 0x000068DC File Offset: 0x00004ADC
		internal void UploadContacts(string accessToken)
		{
			FacebookContactsUploader.Tracer.TraceFunction((long)this.GetHashCode(), "Entering FacebookContactsUploader.UploadContacts.");
			if (string.IsNullOrEmpty(accessToken))
			{
				throw new FacebookContactUploadException(Strings.FacebookEmptyAccessToken);
			}
			bool continueOnContactUploadFailure = this.configuration.ContinueOnContactUploadFailure;
			try
			{
				this.performanceTracker.Start();
				int maximumContactsToUpload = this.configuration.MaximumContactsToUpload;
				IEnumerable<IStorePropertyBag> enumerable = this.contactsEnumerator.Where(new Func<IStorePropertyBag, bool>(this.ShouldExportContact)).Take(maximumContactsToUpload);
				ContactsExporter contactsExporter = new ContactsExporter(FacebookContactsUploader.ContactPropertiesToExport, enumerable);
				using (Stream streamFromContacts = contactsExporter.GetStreamFromContacts())
				{
					this.performanceTracker.AddTimeBookmark(ContactsUploaderPerformanceTrackerBookmarks.ExportTime);
					using (MultipartFormDataContent multipartFormDataContent = FacebookContactsUploader.CreateMultipartFormDataContent(FacebookContactsUploader.MultipartFormDataBoundary, contactsExporter.ContentType, "contacts", streamFromContacts))
					{
						this.performanceTracker.AddTimeBookmark(ContactsUploaderPerformanceTrackerBookmarks.FormatTime);
						using (Stream result = multipartFormDataContent.ReadAsStreamAsync().Result)
						{
							this.performanceTracker.ExportedDataSize = (double)result.Length;
							this.performanceTracker.ReceivedContactsCount = this.UploadContactsToFacebook(accessToken, contactsExporter.Format, FacebookContactsUploader.GetMultipartFormDataContentType(), result);
						}
						this.performanceTracker.AddTimeBookmark(ContactsUploaderPerformanceTrackerBookmarks.UploadTime);
					}
				}
			}
			catch (TimeoutException exception)
			{
				this.ProcessFacebookFailure(exception, Strings.FacebookTimeoutError, !continueOnContactUploadFailure);
			}
			catch (ProtocolException exception2)
			{
				FacebookClient.AppendDiagnoseDataToException(exception2);
				this.ProcessFacebookFailure(exception2, Strings.FacebookAuthorizationError, !continueOnContactUploadFailure);
			}
			catch (CommunicationException exception3)
			{
				FacebookClient.AppendDiagnoseDataToException(exception3);
				this.ProcessFacebookFailure(exception3, Strings.FacebookCommunicationError, !continueOnContactUploadFailure);
			}
			catch (SerializationException exception4)
			{
				this.ProcessFacebookFailure(exception4, Strings.FacebookCommunicationError, !continueOnContactUploadFailure);
			}
			finally
			{
				this.performanceTracker.Stop();
				FacebookContactsUploader.Tracer.TraceFunction((long)this.GetHashCode(), "Leaving FacebookContactsUploader.UploadContacts.");
			}
		}

		// Token: 0x060000EB RID: 235 RVA: 0x00006B5C File Offset: 0x00004D5C
		private int UploadContactsToFacebook(string accessToken, string contactsFormat, string contactsContentType, Stream contactStream)
		{
			FacebookContactsUploader.Tracer.TraceDebug((long)this.GetHashCode(), "Calling FacebookClient.UploadContacts.");
			FacebookImportContactsResult facebookImportContactsResult = this.client.UploadContacts(accessToken, this.configuration.NotifyOnEachContactUpload, this.configuration.WaitForContactUploadCommit, "office365", contactsFormat, contactsContentType, contactStream);
			int num;
			if (facebookImportContactsResult != null)
			{
				num = facebookImportContactsResult.ProcessedContacts;
				FacebookContactsUploader.Tracer.TraceDebug<int>((long)this.GetHashCode(), "Return from FacebookClient.UploadContacts. Number of contacts processed by Facebook {0}.", num);
			}
			else
			{
				num = -1;
				FacebookContactsUploader.Tracer.TraceDebug((long)this.GetHashCode(), "Return from FacebookClient.UploadContacts. No FacebookImportContactsResult was found, unkown number of contacts processed.");
			}
			return num;
		}

		// Token: 0x060000EC RID: 236 RVA: 0x00006BE7 File Offset: 0x00004DE7
		private void ProcessFacebookFailure(Exception exception, LocalizedString defaultDescription, bool shouldThrow)
		{
			FacebookContactsUploader.Tracer.TraceError<Exception>((long)this.GetHashCode(), "Found exception while uploading contacts to Facebook. Exception {0}", exception);
			this.performanceTracker.OperationResult = FacebookContactsUploader.SerializeExceptionForOperationResult(exception);
			if (shouldThrow)
			{
				throw new FacebookContactUploadException(defaultDescription, exception);
			}
		}

		// Token: 0x060000ED RID: 237 RVA: 0x00006C1C File Offset: 0x00004E1C
		private static string SerializeExceptionForOperationResult(Exception exception)
		{
			StringBuilder stringBuilder = new StringBuilder(exception.ToString());
			if (exception.Data != null && exception.Data.Count > 0)
			{
				stringBuilder.Append("-- Data: {");
				foreach (object obj in exception.Data.Keys)
				{
					stringBuilder.Append(string.Format("{0}:{1},", obj, exception.Data[obj]));
				}
				stringBuilder.Append("}");
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060000EE RID: 238 RVA: 0x00006CCC File Offset: 0x00004ECC
		private static MultipartFormDataContent CreateMultipartFormDataContent(string boundary, string contentType, string fieldName, Stream data)
		{
			return new MultipartFormDataContent(boundary)
			{
				{
					new StreamContent(data)
					{
						Headers = 
						{
							{
								"Content-Type",
								contentType
							}
						}
					},
					fieldName,
					FacebookContactsUploader.GenerateContactsFileName()
				}
			};
		}

		// Token: 0x060000EF RID: 239 RVA: 0x00006D08 File Offset: 0x00004F08
		private static string GenerateContactsFileName()
		{
			return string.Format("O365_{0}.csv", Guid.NewGuid().ToString("N"));
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x00006D31 File Offset: 0x00004F31
		private static string GetMultipartFormDataContentType()
		{
			return string.Format("multipart/form-data; boundary={0}", FacebookContactsUploader.MultipartFormDataBoundary);
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x00006D44 File Offset: 0x00004F44
		private bool ShouldExportContact(IStorePropertyBag contact)
		{
			string valueOrDefault = contact.GetValueOrDefault<string>(ContactSchema.PartnerNetworkId, string.Empty);
			bool flag = string.IsNullOrEmpty(valueOrDefault);
			this.performanceTracker.IncrementContactsRead();
			if (flag)
			{
				this.performanceTracker.IncrementContactsExported();
			}
			return flag;
		}

		// Token: 0x04000064 RID: 100
		private const string ContactsSource = "office365";

		// Token: 0x04000065 RID: 101
		private const string ContentTypeHeaderName = "Content-Type";

		// Token: 0x04000066 RID: 102
		private const string ContactsFileNameFormat = "O365_{0}.csv";

		// Token: 0x04000067 RID: 103
		private const string ContactsFieldName = "contacts";

		// Token: 0x04000068 RID: 104
		private const string MultipartFormDataContentTypeFormat = "multipart/form-data; boundary={0}";

		// Token: 0x04000069 RID: 105
		internal static readonly Trace Tracer = ExTraceGlobals.FacebookTracer;

		// Token: 0x0400006A RID: 106
		private static readonly string MultipartFormDataBoundary = string.Format("------------o365-{0}", Guid.NewGuid().ToString("N"));

		// Token: 0x0400006B RID: 107
		private static readonly PropertyDefinition[] ContactPropertiesToExport = new PropertyDefinition[]
		{
			ContactSchema.GivenName,
			ContactSchema.MiddleName,
			ContactSchema.Surname,
			ContactSchema.Email1EmailAddress,
			ContactSchema.Email2EmailAddress,
			ContactSchema.Email3EmailAddress,
			ContactSchema.HomePhone,
			ContactSchema.HomePhone2,
			ContactSchema.BusinessPhoneNumber,
			ContactSchema.BusinessPhoneNumber2,
			ContactSchema.MobilePhone,
			ContactSchema.OtherTelephone,
			ContactSchema.PrimaryTelephoneNumber,
			ContactSchema.HomeStreet,
			ContactSchema.HomeCity,
			ContactSchema.HomeState,
			ContactSchema.HomePostalCode,
			ContactSchema.HomeCountry,
			ContactSchema.OtherStreet,
			ContactSchema.OtherCity,
			ContactSchema.OtherState,
			ContactSchema.OtherPostalCode,
			ContactSchema.OtherCountry,
			ContactSchema.WorkAddressStreet,
			ContactSchema.WorkAddressCity,
			ContactSchema.WorkAddressState,
			ContactSchema.WorkAddressPostalCode,
			ContactSchema.WorkAddressCountry
		};

		// Token: 0x0400006C RID: 108
		private static readonly PropertyDefinition[] AdditionalContactPropertiesToLoad = new PropertyDefinition[]
		{
			ContactSchema.PartnerNetworkId
		};

		// Token: 0x0400006D RID: 109
		private readonly IFacebookClient client;

		// Token: 0x0400006E RID: 110
		private readonly IPeopleConnectApplicationConfig configuration;

		// Token: 0x0400006F RID: 111
		private readonly IEnumerable<IStorePropertyBag> contactsEnumerator;

		// Token: 0x04000070 RID: 112
		private readonly IContactsUploaderPerformanceTracker performanceTracker;
	}
}
