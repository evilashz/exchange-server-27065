using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using Microsoft.Exchange.Compliance.Serialization.Formatters;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.PopImap.Core;

namespace Microsoft.Exchange.Imap4
{
	// Token: 0x0200004E RID: 78
	internal sealed class SubscribeListHelper
	{
		// Token: 0x060002C6 RID: 710 RVA: 0x000141A4 File Offset: 0x000123A4
		internal static bool TryGetList(ResponseFactory factory, out string[] imapSubscribeList, bool forceReloadMailbox = false)
		{
			bool flag;
			return SubscribeListHelper.TryGetList(factory, out imapSubscribeList, out flag, forceReloadMailbox);
		}

		// Token: 0x060002C7 RID: 711 RVA: 0x000141BC File Offset: 0x000123BC
		internal static bool TryModifyList(ResponseFactory factory, SubscribeListHelper.Operation operation, string folderPath)
		{
			string[] array;
			bool flag;
			if (!SubscribeListHelper.TryGetList(factory, out array, out flag, false))
			{
				return false;
			}
			int num = 0;
			while (num < array.Length && !array[num].Equals(folderPath, StringComparison.OrdinalIgnoreCase))
			{
				num++;
			}
			switch (operation)
			{
			case SubscribeListHelper.Operation.Add:
				if (num < array.Length)
				{
					return true;
				}
				Array.Resize<string>(ref array, array.Length + 1);
				array[array.Length - 1] = folderPath;
				break;
			case SubscribeListHelper.Operation.Remove:
			{
				if (num == array.Length)
				{
					return true;
				}
				string[] array2 = new string[array.Length - 1];
				if (num > 0)
				{
					Array.Copy(array, 0, array2, 0, num);
				}
				if (num < array.Length - 1)
				{
					Array.Copy(array, num + 1, array2, num, array.Length - num - 1);
				}
				array = array2;
				break;
			}
			}
			if (flag)
			{
				if (operation != SubscribeListHelper.Operation.Remove)
				{
					goto IL_154;
				}
			}
			try
			{
				factory.Store.Mailbox[MailboxSchema.ImapSubscribeList] = array;
				factory.Store.Mailbox.Save();
				factory.Store.Mailbox.Load(new PropertyDefinition[]
				{
					MailboxSchema.ImapSubscribeList
				});
				if (flag)
				{
					try
					{
						factory.CustomStorageManager.DeleteMailboxConfigurations(new string[]
						{
							"ImapSubscribeLargeList"
						});
					}
					catch (ObjectNotFoundException)
					{
						ProtocolBaseServices.SessionTracer.TraceWarning(factory.Session.SessionId, "ImapSubscribeLargeList does not exist.");
					}
				}
				return true;
			}
			catch (PropertyTooBigException arg)
			{
				ProtocolBaseServices.SessionTracer.TraceWarning<PropertyTooBigException>(factory.Session.SessionId, "List is too large, {0}", arg);
			}
			IL_154:
			using (UserConfiguration orCreateImapSubscribeLargeList = SubscribeListHelper.GetOrCreateImapSubscribeLargeList(factory))
			{
				if (orCreateImapSubscribeLargeList == null)
				{
					ProtocolBaseServices.SessionTracer.TraceError(factory.Session.SessionId, "ImapSubscribeLargeList largeSubscribeListStorage is null");
					return false;
				}
				using (Stream stream = orCreateImapSubscribeLargeList.GetStream())
				{
					if (stream == null)
					{
						ProtocolBaseServices.SessionTracer.TraceError(factory.Session.SessionId, "Stream in ImapSubscribeLargeList largeSubscribeListStorage is null");
						return false;
					}
					BinaryFormatter binaryFormatter = new BinaryFormatter();
					try
					{
						binaryFormatter.Serialize(stream, array);
					}
					catch (SerializationException arg2)
					{
						ProtocolBaseServices.SessionTracer.TraceError<SerializationException>(factory.Session.SessionId, "SerializationException while reading ImapSubscribeLargeList {0}", arg2);
						return false;
					}
					orCreateImapSubscribeLargeList.Save();
					string[] array3 = factory.Store.Mailbox.TryGetProperty(MailboxSchema.ImapSubscribeList) as string[];
					if (array3 != null && array3.Length > 0)
					{
						factory.Store.Mailbox[MailboxSchema.ImapSubscribeList] = new string[0];
						factory.Store.Mailbox.Save();
						factory.Store.Mailbox.Load(new PropertyDefinition[]
						{
							MailboxSchema.ImapSubscribeList
						});
					}
				}
			}
			return true;
		}

		// Token: 0x060002C8 RID: 712 RVA: 0x000144CC File Offset: 0x000126CC
		private static bool TryGetList(ResponseFactory factory, out string[] imapSubscribeList, out bool fromLargeList, bool forceReloadMailbox = false)
		{
			fromLargeList = false;
			if (forceReloadMailbox)
			{
				factory.Store.Mailbox.ForceReload(new PropertyTagPropertyDefinition[]
				{
					MailboxSchema.ImapSubscribeList
				});
			}
			else
			{
				factory.Store.Mailbox.Load(new PropertyTagPropertyDefinition[]
				{
					MailboxSchema.ImapSubscribeList
				});
			}
			object obj = factory.Store.Mailbox.TryGetProperty(MailboxSchema.ImapSubscribeList);
			PropertyError propertyError = obj as PropertyError;
			imapSubscribeList = (obj as string[]);
			if (propertyError != null)
			{
				if (propertyError.PropertyErrorCode == PropertyErrorCode.NotFound)
				{
					if (!SubscribeListHelper.TryGetLargeList(factory, out imapSubscribeList))
					{
						return false;
					}
				}
				else
				{
					ProtocolBaseServices.SessionTracer.TraceError<PropertyError>(factory.Session.SessionId, "Unexpected error while reading ImapSubscribeList {0}", propertyError);
				}
			}
			if (imapSubscribeList != null && imapSubscribeList.Length == 0 && !SubscribeListHelper.TryGetLargeList(factory, out imapSubscribeList))
			{
				return false;
			}
			fromLargeList = (imapSubscribeList.Length > 0);
			return true;
		}

		// Token: 0x060002C9 RID: 713 RVA: 0x00014598 File Offset: 0x00012798
		private static bool TryGetLargeList(ResponseFactory factory, out string[] imapSubscribeList)
		{
			imapSubscribeList = null;
			try
			{
				using (UserConfiguration mailboxConfiguration = factory.CustomStorageManager.GetMailboxConfiguration("ImapSubscribeLargeList", UserConfigurationTypes.Stream))
				{
					if (mailboxConfiguration == null)
					{
						ProtocolBaseServices.SessionTracer.TraceError(factory.Session.SessionId, "ImapSubscribeLargeList largeSubscribeListStorage is null");
						return false;
					}
					using (Stream stream = mailboxConfiguration.GetStream())
					{
						if (stream == null)
						{
							ProtocolBaseServices.SessionTracer.TraceError(factory.Session.SessionId, "Stream in ImapSubscribeLargeList largeSubscribeListStorage is null");
							return false;
						}
						try
						{
							imapSubscribeList = (string[])TypedBinaryFormatter.DeserializeObject(stream, SubscribeListHelper.expectedTypes, null, true);
						}
						catch (BlockedTypeException arg)
						{
							ProtocolBaseServices.SessionTracer.TraceError<BlockedTypeException>(factory.Session.SessionId, "BlockedTypeException while reading ImapSubscribeLargeList {0}", arg);
							return false;
						}
						catch (SerializationException arg2)
						{
							ProtocolBaseServices.SessionTracer.TraceError<SerializationException>(factory.Session.SessionId, "SerializationException while reading ImapSubscribeLargeList {0}", arg2);
							return false;
						}
					}
				}
			}
			catch (ObjectNotFoundException)
			{
				ProtocolBaseServices.SessionTracer.TraceWarning(factory.Session.SessionId, "ImapSubscribeLargeList does not exist, create an empty subscribe list");
				imapSubscribeList = new string[0];
			}
			return true;
		}

		// Token: 0x060002CA RID: 714 RVA: 0x000146E8 File Offset: 0x000128E8
		private static UserConfiguration GetOrCreateImapSubscribeLargeList(ResponseFactory factory)
		{
			UserConfiguration result;
			try
			{
				result = factory.CustomStorageManager.GetMailboxConfiguration("ImapSubscribeLargeList", UserConfigurationTypes.Stream);
			}
			catch (ObjectNotFoundException)
			{
				result = factory.CustomStorageManager.CreateMailboxConfiguration("ImapSubscribeLargeList", UserConfigurationTypes.Stream);
			}
			return result;
		}

		// Token: 0x0400021C RID: 540
		private const string ImapSubscribeLargeList = "ImapSubscribeLargeList";

		// Token: 0x0400021D RID: 541
		private static Type[] expectedTypes = new Type[]
		{
			typeof(string[])
		};

		// Token: 0x0200004F RID: 79
		internal enum Operation
		{
			// Token: 0x0400021F RID: 543
			Add,
			// Token: 0x04000220 RID: 544
			Remove
		}
	}
}
