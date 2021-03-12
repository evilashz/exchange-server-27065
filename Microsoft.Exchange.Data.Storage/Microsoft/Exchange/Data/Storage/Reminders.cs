using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Xml;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000B1D RID: 2845
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[DataContract]
	internal class Reminders<T> where T : IReminder, new()
	{
		// Token: 0x17001C62 RID: 7266
		// (get) Token: 0x0600672E RID: 26414 RVA: 0x001B46D3 File Offset: 0x001B28D3
		// (set) Token: 0x0600672F RID: 26415 RVA: 0x001B46DB File Offset: 0x001B28DB
		[DataMember]
		public int Version { get; private set; }

		// Token: 0x17001C63 RID: 7267
		// (get) Token: 0x06006730 RID: 26416 RVA: 0x001B46E4 File Offset: 0x001B28E4
		// (set) Token: 0x06006731 RID: 26417 RVA: 0x001B46EC File Offset: 0x001B28EC
		[DataMember]
		public List<T> ReminderList
		{
			get
			{
				return this.reminderList;
			}
			set
			{
				Util.ThrowOnNullArgument(value, "ReminderList");
				if (value.Count > 12)
				{
					throw new InvalidParamException(ServerStrings.MaxRemindersExceeded(value.Count, 12));
				}
				this.reminderList = value;
			}
		}

		// Token: 0x06006732 RID: 26418 RVA: 0x001B471D File Offset: 0x001B291D
		public Reminders()
		{
			this.Initialize();
		}

		// Token: 0x06006733 RID: 26419 RVA: 0x001B472C File Offset: 0x001B292C
		public static void Set(IItem item, PropertyDefinition propertyDefinition, Reminders<T> newReminders)
		{
			Util.ThrowOnNullArgument(item, "item");
			Util.ThrowOnNullArgument(propertyDefinition, "propertyDefinition");
			Util.ThrowOnMismatchType<byte[]>(propertyDefinition, "propertyDefinition");
			ExTraceGlobals.RemindersTracer.TraceDebug<StoreObjectId, PropertyDefinition>(0L, "Reminders.Set - item={0}, propertyDefinition={1}", item.StoreObjectId, propertyDefinition);
			if (newReminders == null)
			{
				ExTraceGlobals.RemindersTracer.TraceDebug<PropertyDefinition>(0L, "Reminders.Set - Reminder list is null, deleting property={0}", propertyDefinition);
				item.Delete(propertyDefinition);
				return;
			}
			Reminders<T>.UpdateReminderIdentifiers(newReminders);
			ExTraceGlobals.RemindersTracer.TraceDebug<int>(0L, "Reminders.Set - Serializing reminders, count={0}", newReminders.ReminderList.Count);
			using (Stream stream = item.OpenPropertyStream(propertyDefinition, PropertyOpenMode.Create))
			{
				if (newReminders.ReminderList.Count > 0)
				{
					IReminder reminder = newReminders.ReminderList[0];
					newReminders.Version = reminder.GetCurrentVersion();
				}
				using (XmlWriter xmlWriter = XmlWriter.Create(stream))
				{
					DataContractSerializer dataContractSerializer = new DataContractSerializer(typeof(Reminders<T>));
					dataContractSerializer.WriteObject(xmlWriter, newReminders);
				}
			}
		}

		// Token: 0x06006734 RID: 26420 RVA: 0x001B483C File Offset: 0x001B2A3C
		public static Reminders<T> Get(IItem item, PropertyDefinition propertyDefinition)
		{
			Util.ThrowOnNullArgument(item, "item");
			Util.ThrowOnNullArgument(propertyDefinition, "propertyDefinition");
			ExTraceGlobals.RemindersTracer.TraceDebug<StoreObjectId, PropertyDefinition>(0L, "Reminders.Get - item={0}, propertyDefinition={1}", item.StoreObjectId, propertyDefinition);
			Reminders<T> result;
			try
			{
				using (Stream stream = item.OpenPropertyStream(propertyDefinition, PropertyOpenMode.ReadOnly))
				{
					DataContractSerializer dataContractSerializer = new DataContractSerializer(typeof(Reminders<T>));
					result = (Reminders<T>)dataContractSerializer.ReadObject(stream);
				}
			}
			catch (ObjectNotFoundException arg)
			{
				ExTraceGlobals.RemindersTracer.TraceError<ObjectNotFoundException>(0L, "Reminders.Get - object not found, exception={0}", arg);
				result = null;
			}
			catch (Exception ex)
			{
				ExTraceGlobals.RemindersTracer.TraceError<Exception>(0L, "Reminders.Get - exception={0}", ex);
				if (!Reminders<T>.IsCorruptDataException(ex))
				{
					throw;
				}
				result = new Reminders<T>();
			}
			return result;
		}

		// Token: 0x06006735 RID: 26421 RVA: 0x001B4918 File Offset: 0x001B2B18
		internal static void UpdateReminderIdentifiers(Reminders<T> newReminders)
		{
			ExTraceGlobals.RemindersTracer.TraceDebug(0L, "Reminders.UpdateReminderIdentifiers");
			List<T> list = newReminders.ReminderList;
			Util.ThrowOnNullArgument(list, "newReminderList");
			ExTraceGlobals.RemindersTracer.TraceDebug<int>(0L, "Reminders.UpdateReminderIdentifiers - newReminders count={0}", list.Count);
			foreach (T t in list)
			{
				if (t.Identifier == Guid.Empty)
				{
					ExTraceGlobals.RemindersTracer.TraceDebug(0L, "Generating new reminder identifier");
					t.Identifier = Guid.NewGuid();
				}
			}
		}

		// Token: 0x06006736 RID: 26422 RVA: 0x001B49D8 File Offset: 0x001B2BD8
		public IReminder GetReminder(Guid reminderId)
		{
			foreach (T t in this.ReminderList)
			{
				if (t.Identifier == reminderId)
				{
					return t;
				}
			}
			return null;
		}

		// Token: 0x06006737 RID: 26423 RVA: 0x001B4A48 File Offset: 0x001B2C48
		[OnDeserializing]
		public void OnDeserializing(StreamingContext context)
		{
			this.Initialize();
		}

		// Token: 0x06006738 RID: 26424 RVA: 0x001B4A50 File Offset: 0x001B2C50
		private static bool IsCorruptDataException(Exception e)
		{
			return e is XmlException || e is SerializationException || e is InvalidParamException;
		}

		// Token: 0x06006739 RID: 26425 RVA: 0x001B4A70 File Offset: 0x001B2C70
		private void Initialize()
		{
			T t = (default(T) == null) ? Activator.CreateInstance<T>() : default(T);
			this.Version = t.GetCurrentVersion();
			this.ReminderList = new List<T>();
		}

		// Token: 0x04003A81 RID: 14977
		internal const int MaxReminderCount = 12;

		// Token: 0x04003A82 RID: 14978
		private List<T> reminderList;
	}
}
