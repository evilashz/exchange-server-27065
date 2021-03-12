using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.StoreTasks
{
	// Token: 0x0200079F RID: 1951
	internal class MailboxDiagnosticLogsDataProvider : XsoMailboxDataProviderBase
	{
		// Token: 0x060044BA RID: 17594 RVA: 0x0011A11C File Offset: 0x0011831C
		static MailboxDiagnosticLogsDataProvider()
		{
			PropertyDefinition[] array = new PropertyDefinition[]
			{
				StoreObjectSchema.EntryId,
				StoreObjectSchema.ChangeKey,
				StoreObjectSchema.ParentEntryId,
				StoreObjectSchema.ParentItemId,
				StoreObjectSchema.SearchKey,
				StoreObjectSchema.RecordKey
			};
			MailboxDiagnosticLogsDataProvider.mailboxExtendedProperties = new List<PropertyDefinition>(MailboxSchema.Instance.AllProperties);
			foreach (PropertyDefinition item in array)
			{
				MailboxDiagnosticLogsDataProvider.mailboxExtendedProperties.Remove(item);
			}
		}

		// Token: 0x060044BB RID: 17595 RVA: 0x0011A1B8 File Offset: 0x001183B8
		public MailboxDiagnosticLogsDataProvider(ExchangePrincipal exchangePrincipal, string action) : base(exchangePrincipal, action)
		{
		}

		// Token: 0x060044BC RID: 17596 RVA: 0x0011A1C9 File Offset: 0x001183C9
		public MailboxDiagnosticLogsDataProvider(string componentName, ExchangePrincipal exchangePrincipal, string action) : this(exchangePrincipal, action)
		{
			this.getProperties = false;
			this.componentName = componentName;
		}

		// Token: 0x060044BD RID: 17597 RVA: 0x0011A478 File Offset: 0x00118678
		protected override IEnumerable<T> InternalFindPaged<T>(QueryFilter filter, ObjectId rootId, bool deepSearch, SortBy sortBy, int pageSize)
		{
			if (filter != null && !(filter is FalseFilter))
			{
				throw new NotSupportedException("filter");
			}
			if (rootId != null && rootId is ADObjectId && !ADObjectId.Equals((ADObjectId)rootId, base.MailboxSession.MailboxOwner.ObjectId))
			{
				throw new NotSupportedException("rootId");
			}
			if (!typeof(MailboxDiagnosticLogs).IsAssignableFrom(typeof(T)))
			{
				throw new NotSupportedException("FindPaged: " + typeof(T).FullName);
			}
			MailboxDiagnosticLogs mailboxDiagnosticLog = (MailboxDiagnosticLogs)((object)((default(T) == null) ? Activator.CreateInstance<T>() : default(T)));
			if (this.getProperties)
			{
				mailboxDiagnosticLog.LogName = "ExtendedProperties";
				mailboxDiagnosticLog.MailboxLog = this.ReadMailboxTableProperties();
			}
			else
			{
				mailboxDiagnosticLog.LogName = this.componentName;
				mailboxDiagnosticLog.MailboxLog = this.ReadLogs();
			}
			if (mailboxDiagnosticLog.MailboxLog == null)
			{
				throw new ObjectNotFoundException(Strings.ExportMailboxDiagnosticLogsComponentNotFound(this.componentName ?? "$null", base.MailboxSession.MailboxOwner.MailboxInfo.DisplayName, this.GetAvailableLogNames()));
			}
			mailboxDiagnosticLog[SimpleProviderObjectSchema.Identity] = base.MailboxSession.MailboxOwner.ObjectId;
			yield return (T)((object)mailboxDiagnosticLog);
			yield break;
		}

		// Token: 0x060044BE RID: 17598 RVA: 0x0011A4A3 File Offset: 0x001186A3
		protected override void InternalSave(ConfigurableObject instance)
		{
			throw new NotSupportedException("SaveDiagnosticLog");
		}

		// Token: 0x060044BF RID: 17599 RVA: 0x0011A4AF File Offset: 0x001186AF
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<MailboxDiagnosticLogsDataProvider>(this);
		}

		// Token: 0x060044C0 RID: 17600 RVA: 0x0011A4B8 File Offset: 0x001186B8
		private string ReadMailboxTableProperties()
		{
			List<XElement> list = new List<XElement>();
			base.MailboxSession.Mailbox.Load(MailboxDiagnosticLogsDataProvider.mailboxExtendedProperties);
			object[] properties = base.MailboxSession.Mailbox.GetProperties(MailboxDiagnosticLogsDataProvider.mailboxExtendedProperties);
			int num = 0;
			foreach (PropertyDefinition propertyDefinition in MailboxDiagnosticLogsDataProvider.mailboxExtendedProperties)
			{
				if (!(properties[num] is PropertyError))
				{
					string content;
					if (propertyDefinition.Type.Equals(typeof(byte[])))
					{
						byte[] array = (byte[])properties[num];
						StringBuilder stringBuilder = new StringBuilder((array.Length + 1) * 2);
						stringBuilder.Append("0x");
						foreach (byte b in array)
						{
							stringBuilder.Append(b.ToString("X2"));
						}
						content = stringBuilder.ToString();
					}
					else
					{
						content = properties[num].ToString();
					}
					list.Add(new XElement("Property", new object[]
					{
						new XElement("Name", propertyDefinition.Name),
						new XElement("Value", content)
					}));
				}
				num++;
			}
			XDocument xdocument = new XDocument(new object[]
			{
				new XElement("Properties", new XElement("MailboxTable", list.ToArray()))
			});
			return xdocument.ToString(SaveOptions.None);
		}

		// Token: 0x060044C1 RID: 17601 RVA: 0x0011A670 File Offset: 0x00118870
		private string ReadLogs()
		{
			SingleInstanceItemHandler singleInstanceItemHandler = new SingleInstanceItemHandler(string.Format("IPM.Microsoft.{0}.Log", this.componentName), DefaultFolderType.Configuration);
			return singleInstanceItemHandler.GetItemContent(base.MailboxSession);
		}

		// Token: 0x060044C2 RID: 17602 RVA: 0x0011A6A4 File Offset: 0x001188A4
		private string GetAvailableLogNames()
		{
			StringBuilder stringBuilder = new StringBuilder();
			using (Folder folder = Folder.Bind(base.MailboxSession, DefaultFolderType.Configuration))
			{
				using (QueryResult queryResult = folder.ItemQuery(ItemQueryType.None, null, null, new PropertyDefinition[]
				{
					StoreObjectSchema.ItemClass
				}))
				{
					object[][] rows = queryResult.GetRows(MailboxDiagnosticLogsDataProvider.maxComponentNameListLimit);
					if (rows != null)
					{
						foreach (object[] array2 in rows)
						{
							string itemClass = array2[0] as string;
							string mailboxLogComponentName = this.GetMailboxLogComponentName(itemClass);
							if (mailboxLogComponentName != null)
							{
								stringBuilder.Append((stringBuilder.Length > 0) ? (", " + mailboxLogComponentName) : mailboxLogComponentName);
							}
						}
					}
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060044C3 RID: 17603 RVA: 0x0011A784 File Offset: 0x00118984
		private string GetMailboxLogComponentName(string itemClass)
		{
			MatchCollection matchCollection = MailboxDiagnosticLogsDataProvider.messageClassRegex.Matches(itemClass);
			if (matchCollection.Count == 1)
			{
				return matchCollection[0].Groups["Component"].Value;
			}
			return null;
		}

		// Token: 0x04002A7A RID: 10874
		private const string MessageClass = "IPM.Microsoft.{0}.Log";

		// Token: 0x04002A7B RID: 10875
		private const int ItemClassIndex = 0;

		// Token: 0x04002A7C RID: 10876
		private static int maxComponentNameListLimit = 100;

		// Token: 0x04002A7D RID: 10877
		private static Regex messageClassRegex = new Regex("IPM\\.Microsoft\\.(?<Component>.[^\\.]+?)\\.Log", RegexOptions.IgnoreCase);

		// Token: 0x04002A7E RID: 10878
		private static List<PropertyDefinition> mailboxExtendedProperties = null;

		// Token: 0x04002A7F RID: 10879
		private readonly string componentName;

		// Token: 0x04002A80 RID: 10880
		private readonly bool getProperties = true;
	}
}
