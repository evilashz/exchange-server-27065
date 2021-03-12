using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Xml;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000B1E RID: 2846
	[DataContract]
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class RemindersState<T> where T : IReminderState, new()
	{
		// Token: 0x17001C64 RID: 7268
		// (get) Token: 0x0600673A RID: 26426 RVA: 0x001B4ABC File Offset: 0x001B2CBC
		// (set) Token: 0x0600673B RID: 26427 RVA: 0x001B4AC4 File Offset: 0x001B2CC4
		[DataMember]
		public int Version { get; private set; }

		// Token: 0x17001C65 RID: 7269
		// (get) Token: 0x0600673C RID: 26428 RVA: 0x001B4ACD File Offset: 0x001B2CCD
		// (set) Token: 0x0600673D RID: 26429 RVA: 0x001B4AD5 File Offset: 0x001B2CD5
		[DataMember]
		public List<T> StateList
		{
			get
			{
				return this.stateList;
			}
			set
			{
				Util.ThrowOnNullArgument(value, "StateList");
				this.stateList = value;
			}
		}

		// Token: 0x0600673E RID: 26430 RVA: 0x001B4AE9 File Offset: 0x001B2CE9
		public RemindersState()
		{
			this.Initialize();
		}

		// Token: 0x0600673F RID: 26431 RVA: 0x001B4AF8 File Offset: 0x001B2CF8
		public static void Set(IItem item, PropertyDefinition propertyDefinition, RemindersState<T> newState)
		{
			Util.ThrowOnNullArgument(item, "item");
			Util.ThrowOnNullArgument(propertyDefinition, "propertyDefinition");
			Util.ThrowOnMismatchType<byte[]>(propertyDefinition, "propertyDefinition");
			ExTraceGlobals.RemindersTracer.TraceDebug<StoreObjectId, PropertyDefinition>(0L, "RemindersState.Set - item={0}, propertyDefinition={1}", item.StoreObjectId, propertyDefinition);
			if (newState == null)
			{
				ExTraceGlobals.RemindersTracer.TraceDebug<PropertyDefinition>(0L, "RemindersState.Set - newState is null, deleting property={0}", propertyDefinition);
				item.Delete(propertyDefinition);
				return;
			}
			RemindersState<T>.ValidateStateIdentifiers(newState);
			ExTraceGlobals.RemindersTracer.TraceDebug<int>(0L, "RemindersState.Set - Serializing reminder states, count={0}", newState.StateList.Count);
			using (Stream stream = item.OpenPropertyStream(propertyDefinition, PropertyOpenMode.Create))
			{
				if (newState.StateList.Count > 0)
				{
					IReminderState reminderState = newState.StateList[0];
					newState.Version = reminderState.GetCurrentVersion();
				}
				using (XmlWriter xmlWriter = XmlWriter.Create(stream))
				{
					DataContractSerializer dataContractSerializer = new DataContractSerializer(typeof(RemindersState<T>));
					dataContractSerializer.WriteObject(xmlWriter, newState);
				}
			}
		}

		// Token: 0x06006740 RID: 26432 RVA: 0x001B4C08 File Offset: 0x001B2E08
		public static RemindersState<T> Get(IItem item, PropertyDefinition propertyDefinition)
		{
			Util.ThrowOnNullArgument(item, "item");
			Util.ThrowOnNullArgument(propertyDefinition, "propertyDefinition");
			ExTraceGlobals.RemindersTracer.TraceDebug<StoreObjectId, PropertyDefinition>(0L, "RemindersState.Get - item={0}, propertyDefinition={1}", item.StoreObjectId, propertyDefinition);
			return RemindersState<T>.GetFromStream(new RemindersState<T>.OpenPropertyStreamDelegate(item.OpenPropertyStream), propertyDefinition);
		}

		// Token: 0x06006741 RID: 26433 RVA: 0x001B4C58 File Offset: 0x001B2E58
		public static RemindersState<T> Get(ICorePropertyBag propertyBag, PropertyDefinition propertyDefinition)
		{
			Util.ThrowOnNullArgument(propertyBag, "propertyBag");
			Util.ThrowOnNullArgument(propertyDefinition, "propertyDefinition");
			ExTraceGlobals.RemindersTracer.TraceDebug<PropertyDefinition>(0L, "RemindersState.Get - from property bag propertyDefinition={0}", propertyDefinition);
			return RemindersState<T>.GetFromStream(new RemindersState<T>.OpenPropertyStreamDelegate(propertyBag.OpenPropertyStream), propertyDefinition);
		}

		// Token: 0x06006742 RID: 26434 RVA: 0x001B4CA2 File Offset: 0x001B2EA2
		[OnDeserializing]
		public void OnDeserializing(StreamingContext context)
		{
			this.Initialize();
		}

		// Token: 0x06006743 RID: 26435 RVA: 0x001B4CAC File Offset: 0x001B2EAC
		internal static void ValidateStateIdentifiers(RemindersState<T> newState)
		{
			ExTraceGlobals.RemindersTracer.TraceDebug(0L, "RemindersState.ValidateStateIdentifiers");
			List<T> list = newState.StateList;
			Util.ThrowOnNullArgument(list, "newStateList");
			ExTraceGlobals.RemindersTracer.TraceDebug<int>(0L, "RemindersState.ValidateStateIdentifiers - newStateList count={0}", list.Count);
			foreach (T t in list)
			{
				if (t.Identifier == Guid.Empty)
				{
					throw new ArgumentException("state.Identifier is Guid.Empty", "state.Identifier");
				}
			}
		}

		// Token: 0x06006744 RID: 26436 RVA: 0x001B4D58 File Offset: 0x001B2F58
		private static bool IsCorruptDataException(Exception e)
		{
			return e is XmlException || e is SerializationException || e is InvalidParamException;
		}

		// Token: 0x06006745 RID: 26437 RVA: 0x001B4D78 File Offset: 0x001B2F78
		private void Initialize()
		{
			T t = (default(T) == null) ? Activator.CreateInstance<T>() : default(T);
			this.Version = t.GetCurrentVersion();
			this.StateList = new List<T>();
		}

		// Token: 0x06006746 RID: 26438 RVA: 0x001B4DC4 File Offset: 0x001B2FC4
		private static RemindersState<T> GetFromStream(RemindersState<T>.OpenPropertyStreamDelegate openPropertyStreamMethod, PropertyDefinition propertyDefinition)
		{
			RemindersState<T> result;
			try
			{
				using (Stream stream = openPropertyStreamMethod(propertyDefinition, PropertyOpenMode.ReadOnly))
				{
					DataContractSerializer dataContractSerializer = new DataContractSerializer(typeof(RemindersState<T>));
					result = (RemindersState<T>)dataContractSerializer.ReadObject(stream);
				}
			}
			catch (ObjectNotFoundException arg)
			{
				ExTraceGlobals.RemindersTracer.TraceError<ObjectNotFoundException>(0L, "RemindersState.Get - object not found, exception={0}", arg);
				result = null;
			}
			catch (Exception ex)
			{
				ExTraceGlobals.RemindersTracer.TraceError<Exception>(0L, "RemindersState.Get - exception={0}", ex);
				if (!RemindersState<T>.IsCorruptDataException(ex))
				{
					throw;
				}
				result = new RemindersState<T>();
			}
			return result;
		}

		// Token: 0x04003A84 RID: 14980
		private List<T> stateList;

		// Token: 0x02000B1F RID: 2847
		// (Invoke) Token: 0x06006748 RID: 26440
		private delegate Stream OpenPropertyStreamDelegate(PropertyDefinition propertyDefinition, PropertyOpenMode openMode);
	}
}
