using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Migration.Logging;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x020000CB RID: 203
	internal static class MigrationMessageHelper
	{
		// Token: 0x06000AD2 RID: 2770 RVA: 0x0002D94C File Offset: 0x0002BB4C
		public static XElement GetAttachmentDiagnosticInfo(MessageItem message, MigrationDiagnosticArgument argument)
		{
			XElement xelement = new XElement("Attachments");
			foreach (AttachmentHandle handle in message.AttachmentCollection)
			{
				AttachmentType type = AttachmentType.Stream;
				using (StreamAttachment streamAttachment = (StreamAttachment)message.AttachmentCollection.Open(handle, type))
				{
					string value = streamAttachment.Id.ToBase64String();
					XElement xelement2 = new XElement("Attachment", new object[]
					{
						new XAttribute("name", streamAttachment.FileName),
						new XAttribute("id", value),
						new XAttribute("size", streamAttachment.Size.ToString()),
						new XAttribute("created", streamAttachment.CreationTime.ToString()),
						new XAttribute("lastmodified", streamAttachment.LastModifiedTime.ToString())
					});
					string argument2 = argument.GetArgument<string>("attachmentid");
					if (!string.IsNullOrEmpty(argument2) && argument2.Equals(value, StringComparison.OrdinalIgnoreCase))
					{
						using (StreamReader streamReader = new StreamReader(streamAttachment.GetContentStream(PropertyOpenMode.ReadOnly)))
						{
							xelement2.Add(new XElement("rawdata", streamReader.ReadToEnd()));
						}
					}
					xelement.Add(xelement2);
				}
			}
			return xelement;
		}

		// Token: 0x06000AD3 RID: 2771 RVA: 0x0002DB38 File Offset: 0x0002BD38
		internal static MigrationAttachment CreateAttachment(MessageItem message, string name)
		{
			MigrationUtil.ThrowOnNullArgument(message, "message");
			MigrationUtil.ThrowOnNullOrEmptyArgument(name, "name");
			AttachmentType type = AttachmentType.Stream;
			AttachmentId attachmentId = null;
			foreach (AttachmentHandle handle in message.AttachmentCollection)
			{
				using (StreamAttachment streamAttachment = (StreamAttachment)message.AttachmentCollection.Open(handle, type))
				{
					if (streamAttachment.FileName.Equals(name, StringComparison.OrdinalIgnoreCase))
					{
						attachmentId = streamAttachment.Id;
						break;
					}
				}
			}
			MigrationLogger.Log(MigrationEventType.Information, "Creating a new attachment with name {0}", new object[]
			{
				name
			});
			StreamAttachment streamAttachment2 = (StreamAttachment)message.AttachmentCollection.Create(type);
			streamAttachment2.FileName = name;
			if (attachmentId != null)
			{
				MigrationLogger.Log(MigrationEventType.Information, "Found an existing attachment with name {0} and id {1}, removing old one", new object[]
				{
					name,
					attachmentId.ToBase64String()
				});
				message.AttachmentCollection.Remove(attachmentId);
			}
			return new MigrationAttachment(streamAttachment2, PropertyOpenMode.Create);
		}

		// Token: 0x06000AD4 RID: 2772 RVA: 0x0002DC54 File Offset: 0x0002BE54
		internal static MigrationAttachment GetAttachment(MessageItem message, string name, PropertyOpenMode openMode)
		{
			IMigrationAttachment migrationAttachment = null;
			if (!MigrationMessageHelper.TryGetAttachment(message, name, openMode, out migrationAttachment))
			{
				throw new MigrationAttachmentNotFoundException(name);
			}
			return (MigrationAttachment)migrationAttachment;
		}

		// Token: 0x06000AD5 RID: 2773 RVA: 0x0002DC7C File Offset: 0x0002BE7C
		internal static bool TryGetAttachment(MessageItem message, string name, PropertyOpenMode openMode, out IMigrationAttachment attachment)
		{
			MigrationUtil.ThrowOnNullArgument(message, "message");
			MigrationUtil.ThrowOnNullOrEmptyArgument(name, "name");
			if (openMode != PropertyOpenMode.ReadOnly && openMode != PropertyOpenMode.Modify)
			{
				throw new ArgumentException("Invalid OpenMode for GetAttachment", "openMode");
			}
			attachment = null;
			foreach (AttachmentHandle handle in message.AttachmentCollection)
			{
				StreamAttachment streamAttachment = null;
				try
				{
					AttachmentType type = AttachmentType.Stream;
					streamAttachment = (StreamAttachment)message.AttachmentCollection.Open(handle, type);
					if (streamAttachment.FileName.Equals(name, StringComparison.OrdinalIgnoreCase))
					{
						attachment = new MigrationAttachment(streamAttachment, openMode);
						streamAttachment = null;
						break;
					}
				}
				finally
				{
					if (streamAttachment != null)
					{
						streamAttachment.Dispose();
						streamAttachment = null;
					}
				}
			}
			return attachment != null;
		}

		// Token: 0x06000AD6 RID: 2774 RVA: 0x0002DD48 File Offset: 0x0002BF48
		internal static void DeleteAttachment(MessageItem message, string name)
		{
			MigrationUtil.ThrowOnNullArgument(message, "message");
			MigrationUtil.ThrowOnNullOrEmptyArgument(name, "name");
			List<AttachmentHandle> list = new List<AttachmentHandle>(message.AttachmentCollection.Count);
			foreach (AttachmentHandle attachmentHandle in message.AttachmentCollection)
			{
				AttachmentType type = AttachmentType.Stream;
				using (StreamAttachment streamAttachment = (StreamAttachment)message.AttachmentCollection.Open(attachmentHandle, type))
				{
					if (streamAttachment.FileName.Equals(name, StringComparison.OrdinalIgnoreCase))
					{
						list.Add(attachmentHandle);
					}
				}
			}
			foreach (AttachmentHandle handle in list)
			{
				message.AttachmentCollection.Remove(handle);
			}
		}
	}
}
