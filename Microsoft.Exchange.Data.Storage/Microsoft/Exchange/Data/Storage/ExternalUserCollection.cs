using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Security.Principal;
using System.Text;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000052 RID: 82
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class ExternalUserCollection : ICollection<ExternalUser>, IEnumerable<ExternalUser>, IEnumerable, IDisposeTrackable, IDisposable
	{
		// Token: 0x06000617 RID: 1559 RVA: 0x00032E64 File Offset: 0x00031064
		internal ExternalUserCollection(MailboxSession session)
		{
			this.data = new List<ExternalUser>();
			byte[] entryId = null;
			StoreSession storeSession = null;
			bool flag = false;
			try
			{
				if (storeSession != null)
				{
					storeSession.BeginMapiCall();
					storeSession.BeginServerHealthCall();
					flag = true;
				}
				if (StorageGlobals.MapiTestHookBeforeCall != null)
				{
					StorageGlobals.MapiTestHookBeforeCall(MethodBase.GetCurrentMethod());
				}
				entryId = session.Mailbox.MapiStore.GetLocalDirectoryEntryId();
			}
			catch (MapiPermanentException ex)
			{
				throw StorageGlobals.TranslateMapiException(ServerStrings.MapiCannotLookupEntryId, ex, storeSession, this, "{0}. MapiException = {1}.", new object[]
				{
					string.Format("Unable to lookup Local Directory EntryID", new object[0]),
					ex
				});
			}
			catch (MapiRetryableException ex2)
			{
				throw StorageGlobals.TranslateMapiException(ServerStrings.MapiCannotLookupEntryId, ex2, storeSession, this, "{0}. MapiException = {1}.", new object[]
				{
					string.Format("Unable to lookup Local Directory EntryID", new object[0]),
					ex2
				});
			}
			finally
			{
				try
				{
					if (storeSession != null)
					{
						storeSession.EndMapiCall();
						if (flag)
						{
							storeSession.EndServerHealthCall();
						}
					}
				}
				finally
				{
					if (StorageGlobals.MapiTestHookAfterCall != null)
					{
						StorageGlobals.MapiTestHookAfterCall(MethodBase.GetCurrentMethod());
					}
				}
			}
			StoreObjectId messageId = StoreObjectId.FromProviderSpecificId(entryId, StoreObjectType.Message);
			this.directoryMessage = MessageItem.Bind(session, messageId, new PropertyDefinition[]
			{
				InternalSchema.LocalDirectory
			});
			try
			{
				this.directoryMessage.OpenAsReadWrite();
				try
				{
					using (Stream stream = this.directoryMessage.OpenPropertyStream(InternalSchema.LocalDirectory, PropertyOpenMode.ReadOnly))
					{
						long length = stream.Length;
						using (BinaryReader binaryReader = new BinaryReader(stream))
						{
							while (stream.Position < length)
							{
								ExternalUser item;
								if (ExternalUserCollection.TryReadEntry(binaryReader, out item) && !this.Contains(item))
								{
									this.data.Add(item);
								}
							}
						}
					}
				}
				catch (ObjectNotFoundException)
				{
				}
				catch (EndOfStreamException)
				{
				}
			}
			catch (StoragePermanentException)
			{
				if (this.directoryMessage != null)
				{
					this.directoryMessage.Dispose();
				}
				throw;
			}
			catch (StorageTransientException)
			{
				if (this.directoryMessage != null)
				{
					this.directoryMessage.Dispose();
				}
				throw;
			}
			StorageGlobals.TraceConstructIDisposable(this);
			this.disposeTracker = this.GetDisposeTracker();
		}

		// Token: 0x06000618 RID: 1560 RVA: 0x000330D0 File Offset: 0x000312D0
		private static bool TryReadEntry(BinaryReader reader, out ExternalUser user)
		{
			if (reader.ReadUInt32() == ExternalUserCollection.ptagLocalDirectoryEntryId)
			{
				uint num = reader.ReadUInt32();
				MemoryPropertyBag memoryPropertyBag = new MemoryPropertyBag();
				int num2 = 0;
				while ((long)num2 < (long)((ulong)num))
				{
					uint num3 = reader.ReadUInt32();
					object obj = ExternalUserCollection.ReadPropValue(reader, ((PropTag)num3).ValueType(), reader.ReadUInt32());
					if (obj != null)
					{
						PropertyTagPropertyDefinition prop = PropertyTagPropertyDefinition.CreateCustom(string.Empty, num3);
						memoryPropertyBag.PreLoadStoreProperty(prop, obj, (int)num);
					}
					num2++;
				}
				memoryPropertyBag.SetAllPropertiesLoaded();
				IDirectPropertyBag directPropertyBag = memoryPropertyBag;
				if (directPropertyBag.IsLoaded(InternalSchema.MemberSIDLocalDirectory) && directPropertyBag.IsLoaded(InternalSchema.MemberExternalIdLocalDirectory) && directPropertyBag.IsLoaded(InternalSchema.MemberEmailLocalDirectory))
				{
					if (!directPropertyBag.IsLoaded(InternalSchema.MemberName))
					{
						memoryPropertyBag[InternalSchema.MemberName] = directPropertyBag.GetValue(InternalSchema.MemberEmailLocalDirectory);
					}
					user = new ExternalUser(memoryPropertyBag);
					return true;
				}
			}
			user = null;
			return false;
		}

		// Token: 0x06000619 RID: 1561 RVA: 0x000331A8 File Offset: 0x000313A8
		private static object ReadPropValue(BinaryReader reader, PropType type, uint len)
		{
			if (type <= PropType.Binary)
			{
				if (type == PropType.Int)
				{
					return ExternalUserCollection.ReadInt(reader, len);
				}
				if (type == PropType.String)
				{
					return ExternalUserCollection.ReadString(reader, len);
				}
				if (type == PropType.Binary)
				{
					return ExternalUserCollection.ReadBytes(reader, len);
				}
			}
			else
			{
				if (type == PropType.IntArray)
				{
					return ExternalUserCollection.ReadArrayValue<int>(reader, len, new ExternalUserCollection.ReadValue<int>(ExternalUserCollection.ReadInt));
				}
				if (type == PropType.StringArray)
				{
					return ExternalUserCollection.ReadArrayValue<string>(reader, len, new ExternalUserCollection.ReadValue<string>(ExternalUserCollection.ReadString));
				}
				if (type == PropType.BinaryArray)
				{
					return ExternalUserCollection.ReadArrayValue<byte[]>(reader, len, new ExternalUserCollection.ReadValue<byte[]>(ExternalUserCollection.ReadBytes));
				}
			}
			return null;
		}

		// Token: 0x0600061A RID: 1562 RVA: 0x00033248 File Offset: 0x00031448
		private static T[] ReadArrayValue<T>(BinaryReader reader, uint len, ExternalUserCollection.ReadValue<T> readCall)
		{
			T[] array = new T[len];
			int num = 0;
			while ((long)num < (long)((ulong)len))
			{
				uint len2 = reader.ReadUInt32();
				array[num] = readCall(reader, len2);
				num++;
			}
			return array;
		}

		// Token: 0x0600061B RID: 1563 RVA: 0x00033284 File Offset: 0x00031484
		private static string ReadString(BinaryReader reader, uint len)
		{
			string text = new string(Encoding.Unicode.GetChars(ExternalUserCollection.ReadBytes(reader, len)));
			string text2 = text;
			char[] trimChars = new char[1];
			return text2.TrimEnd(trimChars);
		}

		// Token: 0x0600061C RID: 1564 RVA: 0x000332B8 File Offset: 0x000314B8
		private static byte[] ReadBytes(BinaryReader reader, uint len)
		{
			byte[] result = reader.ReadBytes((int)len);
			if (len % 4U != 0U)
			{
				reader.ReadBytes((int)(len % 4U));
			}
			return result;
		}

		// Token: 0x0600061D RID: 1565 RVA: 0x000332DD File Offset: 0x000314DD
		private static int ReadInt(BinaryReader reader, uint len)
		{
			return reader.ReadInt32();
		}

		// Token: 0x0600061E RID: 1566 RVA: 0x000332E8 File Offset: 0x000314E8
		private static void WriteArrayValue<T>(BinaryWriter writer, T[] value, ExternalUserCollection.WriteValue<T> callback)
		{
			writer.Write((uint)value.Length);
			for (int i = 0; i < value.Length; i++)
			{
				callback(writer, value[i]);
			}
		}

		// Token: 0x0600061F RID: 1567 RVA: 0x0003331C File Offset: 0x0003151C
		private static void WriteStringValue(BinaryWriter writer, string value)
		{
			Encoding unicode = Encoding.Unicode;
			char[] trimChars = new char[1];
			byte[] bytes = unicode.GetBytes(value.TrimEnd(trimChars));
			int num = (bytes.Length + 2) % 4 + 2;
			writer.Write((uint)(bytes.Length + num));
			writer.Write(bytes);
			for (int i = 0; i < num; i++)
			{
				writer.Write(0);
			}
		}

		// Token: 0x06000620 RID: 1568 RVA: 0x00033374 File Offset: 0x00031574
		private static void WriteByteValue(BinaryWriter writer, byte[] value)
		{
			writer.Write((uint)value.Length);
			writer.Write(value);
			int num = value.Length % 4;
			for (int i = 0; i < num; i++)
			{
				writer.Write(0);
			}
		}

		// Token: 0x06000621 RID: 1569 RVA: 0x000333AA File Offset: 0x000315AA
		private static void WriteIntValue(BinaryWriter writer, int value)
		{
			writer.Write(4U);
			writer.Write(value);
		}

		// Token: 0x06000622 RID: 1570 RVA: 0x000333BC File Offset: 0x000315BC
		private static void WritePropValue(BinaryWriter writer, PropertyDefinition prop, MemoryPropertyBag propertyBag)
		{
			PropertyTagPropertyDefinition propertyTagPropertyDefinition = (PropertyTagPropertyDefinition)prop;
			writer.Write(propertyTagPropertyDefinition.PropertyTag);
			object obj = propertyBag.TryGetProperty(propertyTagPropertyDefinition);
			PropType propType = ((PropTag)propertyTagPropertyDefinition.PropertyTag).ValueType();
			PropType propType2 = propType;
			if (propType2 <= PropType.Binary)
			{
				if (propType2 == PropType.Int)
				{
					ExternalUserCollection.WriteIntValue(writer, (int)obj);
					return;
				}
				if (propType2 == PropType.String)
				{
					ExternalUserCollection.WriteStringValue(writer, (string)obj);
					return;
				}
				if (propType2 == PropType.Binary)
				{
					ExternalUserCollection.WriteByteValue(writer, (byte[])obj);
					return;
				}
			}
			else
			{
				if (propType2 == PropType.IntArray)
				{
					ExternalUserCollection.WriteArrayValue<int>(writer, (int[])obj, new ExternalUserCollection.WriteValue<int>(ExternalUserCollection.WriteIntValue));
					return;
				}
				if (propType2 == PropType.StringArray)
				{
					ExternalUserCollection.WriteArrayValue<string>(writer, (string[])obj, new ExternalUserCollection.WriteValue<string>(ExternalUserCollection.WriteStringValue));
					return;
				}
				if (propType2 == PropType.BinaryArray)
				{
					ExternalUserCollection.WriteArrayValue<byte[]>(writer, (byte[][])obj, new ExternalUserCollection.WriteValue<byte[]>(ExternalUserCollection.WriteByteValue));
					return;
				}
			}
			writer.Write(0U);
		}

		// Token: 0x06000623 RID: 1571 RVA: 0x000334A3 File Offset: 0x000316A3
		IEnumerator IEnumerable.GetEnumerator()
		{
			this.CheckDisposed("GetEnumerator");
			return this.data.GetEnumerator();
		}

		// Token: 0x06000624 RID: 1572 RVA: 0x000334C0 File Offset: 0x000316C0
		IEnumerator<ExternalUser> IEnumerable<ExternalUser>.GetEnumerator()
		{
			this.CheckDisposed("GetEnumerator");
			return this.data.GetEnumerator();
		}

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x06000625 RID: 1573 RVA: 0x000334DD File Offset: 0x000316DD
		public int Count
		{
			get
			{
				this.CheckDisposed("Count::get");
				return this.data.Count;
			}
		}

		// Token: 0x17000133 RID: 307
		// (get) Token: 0x06000626 RID: 1574 RVA: 0x000334F5 File Offset: 0x000316F5
		public bool IsReadOnly
		{
			get
			{
				this.CheckDisposed("IsReadOnly::get");
				return false;
			}
		}

		// Token: 0x06000627 RID: 1575 RVA: 0x00033504 File Offset: 0x00031704
		public void Add(ExternalUser item)
		{
			this.CheckDisposed("Add");
			if (item == null)
			{
				throw new ArgumentNullException();
			}
			if (this.Count >= 1000)
			{
				throw new InvalidOperationException("Cannot add any more users to the ExternalUserCollection.");
			}
			if (this.Contains(item))
			{
				throw new ArgumentException("User is already present in the collection.");
			}
			this.data.Add(item);
			this.isDirty = true;
		}

		// Token: 0x06000628 RID: 1576 RVA: 0x00033564 File Offset: 0x00031764
		public void Clear()
		{
			this.CheckDisposed("Clear");
			if (this.data.Count > 0)
			{
				this.data.Clear();
				this.isDirty = true;
			}
		}

		// Token: 0x06000629 RID: 1577 RVA: 0x00033594 File Offset: 0x00031794
		public bool Contains(ExternalUser item)
		{
			this.CheckDisposed("Contains");
			if (item == null)
			{
				throw new ArgumentNullException();
			}
			foreach (ExternalUser externalUser in this.data)
			{
				if (externalUser.SmtpAddress.Equals(item.SmtpAddress) || externalUser.Sid.Equals(item.Sid) || externalUser.ExternalId == item.ExternalId)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600062A RID: 1578 RVA: 0x00033638 File Offset: 0x00031838
		public void CopyTo(ExternalUser[] array, int arrayIndex)
		{
			this.CheckDisposed("CopyTo");
			if (arrayIndex < 0)
			{
				throw new ArgumentOutOfRangeException();
			}
			if (array == null)
			{
				throw new ArgumentNullException();
			}
			if (array.Length - arrayIndex > this.data.Count)
			{
				throw new ArgumentException();
			}
			foreach (ExternalUser externalUser in this.data)
			{
				array[arrayIndex] = externalUser;
				arrayIndex++;
			}
		}

		// Token: 0x0600062B RID: 1579 RVA: 0x00033700 File Offset: 0x00031900
		public bool Remove(ExternalUser item)
		{
			this.CheckDisposed("Remove");
			if (item == null)
			{
				throw new ArgumentNullException();
			}
			if (this.data.Remove(this.data.Find((ExternalUser obj) => obj.Sid.Equals(item.Sid) || obj.ExternalId == item.ExternalId)))
			{
				this.isDirty = true;
				return true;
			}
			return false;
		}

		// Token: 0x0600062C RID: 1580 RVA: 0x00033764 File Offset: 0x00031964
		private static string CreateExternalIdentity(SmtpAddress smtpAddress)
		{
			byte[] array = new byte[24];
			Util.GetRandomBytes(array);
			string str = Convert.ToBase64String(array);
			return str + "@" + smtpAddress.Domain;
		}

		// Token: 0x0600062D RID: 1581 RVA: 0x00033798 File Offset: 0x00031998
		public ExternalUser AddFederatedUser(SmtpAddress smtpAddress)
		{
			return this.InternalAdd(smtpAddress, false);
		}

		// Token: 0x0600062E RID: 1582 RVA: 0x000337A2 File Offset: 0x000319A2
		public ExternalUser AddReachUser(SmtpAddress smtpAddress)
		{
			return this.InternalAdd(smtpAddress, true);
		}

		// Token: 0x0600062F RID: 1583 RVA: 0x000337AC File Offset: 0x000319AC
		public ExternalUser FindReachUserWithOriginalSmtpAddress(SmtpAddress originalSmtpAddress)
		{
			this.CheckDisposed("FindReachUserWithOriginalSmtpAddress");
			SmtpAddress smtpAddress = ExternalUser.ConvertToReachSmtpAddress(originalSmtpAddress);
			ExternalUser externalUser = this.FindExternalUser(smtpAddress);
			if (externalUser != null && !externalUser.IsReachUser)
			{
				externalUser = null;
			}
			return externalUser;
		}

		// Token: 0x06000630 RID: 1584 RVA: 0x000337E4 File Offset: 0x000319E4
		public ExternalUser FindFederatedUserWithOriginalSmtpAddress(SmtpAddress originalSmtpAddress)
		{
			this.CheckDisposed("FindFederatedUserWithOriginalSmtpAddress");
			ExternalUser externalUser = this.FindExternalUser(originalSmtpAddress);
			if (externalUser != null && externalUser.IsReachUser)
			{
				externalUser = null;
			}
			return externalUser;
		}

		// Token: 0x06000631 RID: 1585 RVA: 0x00033830 File Offset: 0x00031A30
		public ExternalUser FindExternalUser(SmtpAddress smtpAddress)
		{
			this.CheckDisposed("FindExternalUser");
			return this.data.Find((ExternalUser obj) => obj.SmtpAddress == smtpAddress);
		}

		// Token: 0x06000632 RID: 1586 RVA: 0x00033888 File Offset: 0x00031A88
		public ExternalUser FindExternalUser(string externalId)
		{
			this.CheckDisposed("FindExternalUser");
			if (externalId == null)
			{
				throw new ArgumentNullException();
			}
			return this.data.Find((ExternalUser obj) => obj.ExternalId == externalId);
		}

		// Token: 0x06000633 RID: 1587 RVA: 0x000338F0 File Offset: 0x00031AF0
		public ExternalUser FindExternalUser(SecurityIdentifier sid)
		{
			this.CheckDisposed("FindExternalUser");
			if (sid == null)
			{
				throw new ArgumentNullException();
			}
			return this.data.Find((ExternalUser obj) => obj.Sid.Equals(sid));
		}

		// Token: 0x06000634 RID: 1588 RVA: 0x00033940 File Offset: 0x00031B40
		public SaveResult Save()
		{
			this.CheckDisposed("Save");
			if (!this.isDirty)
			{
				bool flag = false;
				foreach (ExternalUser externalUser in this.data)
				{
					flag |= externalUser.PropertyBag.IsDirty;
				}
				if (!flag)
				{
					return SaveResult.Success;
				}
			}
			using (Stream stream = this.directoryMessage.OpenPropertyStream(InternalSchema.LocalDirectory, PropertyOpenMode.Create))
			{
				using (BinaryWriter binaryWriter = new BinaryWriter(stream))
				{
					foreach (ExternalUser externalUser2 in this.data)
					{
						binaryWriter.Write(ExternalUserCollection.ptagLocalDirectoryEntryId);
						ICollection<PropertyDefinition> allFoundProperties = externalUser2.PropertyBag.AllFoundProperties;
						binaryWriter.Write((uint)allFoundProperties.Count);
						ExternalUserCollection.WritePropValue(binaryWriter, InternalSchema.MemberSIDLocalDirectory, externalUser2.PropertyBag);
						foreach (PropertyDefinition propertyDefinition in allFoundProperties)
						{
							if (propertyDefinition != InternalSchema.MemberSIDLocalDirectory)
							{
								ExternalUserCollection.WritePropValue(binaryWriter, propertyDefinition, externalUser2.PropertyBag);
							}
						}
					}
				}
			}
			ConflictResolutionResult conflictResolutionResult = this.directoryMessage.Save(SaveMode.ResolveConflicts);
			return conflictResolutionResult.SaveStatus;
		}

		// Token: 0x06000635 RID: 1589 RVA: 0x00033AE0 File Offset: 0x00031CE0
		public DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<ExternalUserCollection>(this);
		}

		// Token: 0x06000636 RID: 1590 RVA: 0x00033AE8 File Offset: 0x00031CE8
		public void SuppressDisposeTracker()
		{
			if (this.disposeTracker != null)
			{
				this.disposeTracker.Suppress();
			}
		}

		// Token: 0x06000637 RID: 1591 RVA: 0x00033AFD File Offset: 0x00031CFD
		private void CheckDisposed(string methodName)
		{
			if (this.isDisposed)
			{
				StorageGlobals.TraceFailedCheckDisposed(this, methodName);
				throw new ObjectDisposedException(base.GetType().ToString());
			}
		}

		// Token: 0x06000638 RID: 1592 RVA: 0x00033B1F File Offset: 0x00031D1F
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000639 RID: 1593 RVA: 0x00033B30 File Offset: 0x00031D30
		private ExternalUser InternalAdd(SmtpAddress smtpAddress, bool isReachUser)
		{
			this.CheckDisposed("InternalAdd");
			string externalId = ExternalUserCollection.CreateExternalIdentity(smtpAddress);
			ExternalUser externalUser = new ExternalUser(externalId, smtpAddress, isReachUser);
			this.Add(externalUser);
			return externalUser;
		}

		// Token: 0x0600063A RID: 1594 RVA: 0x00033B60 File Offset: 0x00031D60
		private void Dispose(bool disposing)
		{
			StorageGlobals.TraceDispose(this, this.isDisposed, disposing);
			if (!this.isDisposed)
			{
				this.isDisposed = true;
				this.InternalDispose(disposing);
			}
		}

		// Token: 0x0600063B RID: 1595 RVA: 0x00033B85 File Offset: 0x00031D85
		private void InternalDispose(bool disposing)
		{
			if (disposing)
			{
				if (this.directoryMessage != null)
				{
					this.directoryMessage.Dispose();
				}
				if (this.disposeTracker != null)
				{
					this.disposeTracker.Dispose();
				}
			}
		}

		// Token: 0x040001CC RID: 460
		internal const int MaxLimit = 1000;

		// Token: 0x040001CD RID: 461
		private readonly MessageItem directoryMessage;

		// Token: 0x040001CE RID: 462
		private readonly List<ExternalUser> data;

		// Token: 0x040001CF RID: 463
		private bool isDisposed;

		// Token: 0x040001D0 RID: 464
		private bool isDirty;

		// Token: 0x040001D1 RID: 465
		private readonly DisposeTracker disposeTracker;

		// Token: 0x040001D2 RID: 466
		private static uint ptagLocalDirectoryEntryId = 873857282U;

		// Token: 0x02000053 RID: 83
		// (Invoke) Token: 0x0600063E RID: 1598
		private delegate T ReadValue<T>(BinaryReader reader, uint len);

		// Token: 0x02000054 RID: 84
		// (Invoke) Token: 0x06000642 RID: 1602
		private delegate void WriteValue<T>(BinaryWriter writer, T value);
	}
}
