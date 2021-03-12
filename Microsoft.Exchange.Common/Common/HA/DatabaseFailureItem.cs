using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Eventing.Reader;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Cluster.Replay;

namespace Microsoft.Exchange.Common.HA
{
	// Token: 0x0200003F RID: 63
	internal class DatabaseFailureItem
	{
		// Token: 0x06000127 RID: 295 RVA: 0x00006C12 File Offset: 0x00004E12
		internal DatabaseFailureItem()
		{
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000128 RID: 296 RVA: 0x00006C1A File Offset: 0x00004E1A
		// (set) Token: 0x06000129 RID: 297 RVA: 0x00006C22 File Offset: 0x00004E22
		internal FailureNameSpace NameSpace
		{
			get
			{
				return this.nameSpace;
			}
			set
			{
				this.nameSpace = value;
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x0600012A RID: 298 RVA: 0x00006C2B File Offset: 0x00004E2B
		// (set) Token: 0x0600012B RID: 299 RVA: 0x00006C33 File Offset: 0x00004E33
		internal FailureTag Tag
		{
			get
			{
				return this.tag;
			}
			set
			{
				this.tag = value;
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x0600012C RID: 300 RVA: 0x00006C3C File Offset: 0x00004E3C
		// (set) Token: 0x0600012D RID: 301 RVA: 0x00006C44 File Offset: 0x00004E44
		internal Guid Guid
		{
			get
			{
				return this.guid;
			}
			set
			{
				this.guid = value;
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x0600012E RID: 302 RVA: 0x00006C4D File Offset: 0x00004E4D
		// (set) Token: 0x0600012F RID: 303 RVA: 0x00006C55 File Offset: 0x00004E55
		internal string InstanceName
		{
			get
			{
				return this.instanceName;
			}
			set
			{
				this.instanceName = value;
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000130 RID: 304 RVA: 0x00006C5E File Offset: 0x00004E5E
		// (set) Token: 0x06000131 RID: 305 RVA: 0x00006C66 File Offset: 0x00004E66
		internal string ComponentName
		{
			get
			{
				return this.componentName;
			}
			set
			{
				this.componentName = value;
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000132 RID: 306 RVA: 0x00006C6F File Offset: 0x00004E6F
		// (set) Token: 0x06000133 RID: 307 RVA: 0x00006C77 File Offset: 0x00004E77
		internal IoErrorInfo IoError
		{
			get
			{
				return this.ioError;
			}
			set
			{
				this.ioError = value;
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000134 RID: 308 RVA: 0x00006C80 File Offset: 0x00004E80
		// (set) Token: 0x06000135 RID: 309 RVA: 0x00006C88 File Offset: 0x00004E88
		internal NotificationEventInfo NotifyEvent
		{
			get
			{
				return this.notifyEvent;
			}
			set
			{
				this.notifyEvent = value;
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000136 RID: 310 RVA: 0x00006C91 File Offset: 0x00004E91
		// (set) Token: 0x06000137 RID: 311 RVA: 0x00006C99 File Offset: 0x00004E99
		internal DateTime CreationTime
		{
			get
			{
				return this.creationTime;
			}
			set
			{
				this.creationTime = value;
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000138 RID: 312 RVA: 0x00006CA2 File Offset: 0x00004EA2
		// (set) Token: 0x06000139 RID: 313 RVA: 0x00006CAA File Offset: 0x00004EAA
		internal EventBookmark Bookmark
		{
			get
			{
				return this.bookMark;
			}
			set
			{
				this.bookMark = value;
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x0600013A RID: 314 RVA: 0x00006CB3 File Offset: 0x00004EB3
		// (set) Token: 0x0600013B RID: 315 RVA: 0x00006CBB File Offset: 0x00004EBB
		internal string Message
		{
			get
			{
				return this.message;
			}
			set
			{
				this.message = value;
			}
		}

		// Token: 0x0600013C RID: 316 RVA: 0x00006CC4 File Offset: 0x00004EC4
		internal DatabaseFailureItem(FailureNameSpace nameSpace, FailureTag tag, Guid guid)
		{
			this.NameSpace = nameSpace;
			this.Tag = tag;
			this.Guid = guid;
		}

		// Token: 0x0600013D RID: 317 RVA: 0x00006CE1 File Offset: 0x00004EE1
		internal DatabaseFailureItem(FailureNameSpace nameSpace, FailureTag tag, Guid guid, string message)
		{
			this.NameSpace = nameSpace;
			this.Tag = tag;
			this.Guid = guid;
			this.Message = message;
		}

		// Token: 0x0600013E RID: 318 RVA: 0x00006D08 File Offset: 0x00004F08
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			if (base.GetType() != obj.GetType())
			{
				return false;
			}
			DatabaseFailureItem databaseFailureItem = obj as DatabaseFailureItem;
			return this.NameSpace.Equals(databaseFailureItem.NameSpace) && this.Tag.Equals(databaseFailureItem.Tag) && this.Guid.Equals(databaseFailureItem.Guid) && string.Equals(this.InstanceName, databaseFailureItem.InstanceName, StringComparison.OrdinalIgnoreCase) && ((string.IsNullOrEmpty(this.ComponentName) && string.IsNullOrEmpty(databaseFailureItem.ComponentName)) || string.Equals(this.ComponentName, databaseFailureItem.ComponentName, StringComparison.OrdinalIgnoreCase)) && object.Equals(this.IoError, databaseFailureItem.IoError) && object.Equals(this.NotifyEvent, databaseFailureItem.NotifyEvent) && ((string.IsNullOrEmpty(this.Message) && string.IsNullOrEmpty(databaseFailureItem.Message)) || string.Equals(this.Message, databaseFailureItem.Message, StringComparison.OrdinalIgnoreCase));
		}

		// Token: 0x0600013F RID: 319 RVA: 0x00006E2D File Offset: 0x0000502D
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06000140 RID: 320 RVA: 0x00006E38 File Offset: 0x00005038
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(256);
			stringBuilder.AppendFormat("CreationTime={0} ", this.CreationTime);
			stringBuilder.Append(Environment.NewLine);
			stringBuilder.AppendFormat("Namespace={0} ", this.NameSpace);
			stringBuilder.Append(Environment.NewLine);
			stringBuilder.AppendFormat("Tag={0} ", this.Tag);
			stringBuilder.Append(Environment.NewLine);
			stringBuilder.AppendFormat("Guid={0} ", this.Guid);
			stringBuilder.Append(Environment.NewLine);
			stringBuilder.AppendFormat("InstanceName={0} ", this.InstanceName);
			stringBuilder.Append(Environment.NewLine);
			stringBuilder.AppendFormat("ComponentName={0} ", this.ComponentName ?? "<null>");
			stringBuilder.Append(Environment.NewLine);
			stringBuilder.Append("IoError=");
			if (this.IoError != null)
			{
				stringBuilder.Append(this.IoError.ToString());
			}
			else
			{
				stringBuilder.Append("<null>");
			}
			stringBuilder.AppendLine();
			stringBuilder.Append("NotificationEventInfo=");
			if (this.NotifyEvent != null)
			{
				stringBuilder.Append(this.NotifyEvent.ToString());
			}
			else
			{
				stringBuilder.Append("<null>");
				stringBuilder.AppendLine();
			}
			stringBuilder.AppendFormat("Message={0} ", this.Message ?? "<null>");
			stringBuilder.Append(Environment.NewLine);
			return stringBuilder.ToString();
		}

		// Token: 0x06000141 RID: 321 RVA: 0x00006FC4 File Offset: 0x000051C4
		internal static DatabaseFailureItem Parse(string xml, bool isValidate)
		{
			DatabaseFailureItem databaseFailureItem = new DatabaseFailureItem();
			if (isValidate)
			{
				databaseFailureItem.Validate();
			}
			return databaseFailureItem;
		}

		// Token: 0x06000142 RID: 322 RVA: 0x00006FE4 File Offset: 0x000051E4
		internal static DatabaseFailureItem Parse(EventRecord record)
		{
			DatabaseFailureItem databaseFailureItem = new DatabaseFailureItem();
			IList<EventProperty> properties = record.Properties;
			if (properties == null || properties.Count == 0)
			{
				return databaseFailureItem;
			}
			DatabaseFailureItem.PropertyContainer propertyContainer = new DatabaseFailureItem.PropertyContainer(properties);
			int num = propertyContainer.Get<int>(EventRecordParameteIndex.Version);
			if (num != DatabaseFailureItem.ApiVersion)
			{
				throw new InvalidFailureItemException("Version");
			}
			databaseFailureItem.NameSpace = propertyContainer.GetEnum<FailureNameSpace>(EventRecordParameteIndex.Namespace);
			databaseFailureItem.Tag = propertyContainer.GetEnum<FailureTag>(EventRecordParameteIndex.Tag);
			databaseFailureItem.Guid = propertyContainer.Get<Guid>(EventRecordParameteIndex.Guid);
			databaseFailureItem.InstanceName = propertyContainer.Get<string>(EventRecordParameteIndex.InstanceName);
			databaseFailureItem.ComponentName = propertyContainer.Get<string>(EventRecordParameteIndex.ComponentName);
			databaseFailureItem.CreationTime = (record.TimeCreated ?? DateTime.MinValue);
			bool flag = propertyContainer.Get<bool>(EventRecordParameteIndex.IsIoErrorSpecified);
			bool flag2 = propertyContainer.Get<bool>(EventRecordParameteIndex.IsNotifyEventSpecified);
			if (flag)
			{
				databaseFailureItem.IoError = new IoErrorInfo
				{
					Category = propertyContainer.GetEnum<IoErrorCategory>(EventRecordParameteIndex.IoErrorCategory),
					FileName = propertyContainer.Get<string>(EventRecordParameteIndex.IoErrorFileName),
					Offset = propertyContainer.Get<long>(EventRecordParameteIndex.IoErrorOffset),
					Size = propertyContainer.Get<long>(EventRecordParameteIndex.IoErrorSize)
				};
			}
			if (flag2)
			{
				NotificationEventInfo notificationEventInfo = new NotificationEventInfo();
				notificationEventInfo.EventId = propertyContainer.Get<int>(EventRecordParameteIndex.NotifyeventId);
				uint num2 = propertyContainer.Get<uint>(EventRecordParameteIndex.NotifyeventParambufferSize);
				if (num2 > 0U)
				{
					byte[] bytes = propertyContainer.Get<byte[]>(EventRecordParameteIndex.NotifyeventParambuffer);
					UnicodeEncoding unicodeEncoding = new UnicodeEncoding();
					string @string = unicodeEncoding.GetString(bytes);
					string text = @string;
					char[] separator = new char[1];
					string[] array = text.Split(separator);
					notificationEventInfo.Parameters = new string[array.Length - 1];
					for (int i = 0; i < array.Length - 1; i++)
					{
						notificationEventInfo.Parameters[i] = array[i];
					}
				}
				databaseFailureItem.NotifyEvent = notificationEventInfo;
			}
			databaseFailureItem.Bookmark = record.Bookmark;
			if (propertyContainer.IsIndexValid(EventRecordParameteIndex.Message))
			{
				databaseFailureItem.Message = propertyContainer.Get<string>(EventRecordParameteIndex.Message);
			}
			return databaseFailureItem;
		}

		// Token: 0x06000143 RID: 323 RVA: 0x000071B1 File Offset: 0x000053B1
		internal static DatabaseFailureItem[] GetEntriesFromEventLog()
		{
			return DatabaseFailureItem.GetEntriesFromEventLog(DatabaseFailureItem.ChannelName, DatabaseFailureItem.DefaultQueryString);
		}

		// Token: 0x06000144 RID: 324 RVA: 0x000071C4 File Offset: 0x000053C4
		internal static DatabaseFailureItem[] GetEntriesFromEventLog(string channelName, string queryString)
		{
			List<DatabaseFailureItem> list = new List<DatabaseFailureItem>();
			EventLogQuery eventQuery = new EventLogQuery(channelName, PathType.LogName, queryString);
			DatabaseFailureItem[] result;
			using (EventLogReader eventLogReader = new EventLogReader(eventQuery))
			{
				for (;;)
				{
					using (EventRecord eventRecord = eventLogReader.ReadEvent())
					{
						if (eventRecord != null)
						{
							DatabaseFailureItem databaseFailureItem = DatabaseFailureItem.Parse(eventRecord);
							if (databaseFailureItem != null)
							{
								list.Add(databaseFailureItem);
							}
							continue;
						}
					}
					break;
				}
				result = list.ToArray();
			}
			return result;
		}

		// Token: 0x06000145 RID: 325 RVA: 0x00007248 File Offset: 0x00005448
		internal static IDisposable SetPublishDatabaseFailureItemTestHook(Action action)
		{
			return DatabaseFailureItem.publishDatabaseFailureItemTestHook.SetTestHook(action);
		}

		// Token: 0x06000146 RID: 326 RVA: 0x00007255 File Offset: 0x00005455
		internal void Publish()
		{
			this.Publish(true, false);
		}

		// Token: 0x06000147 RID: 327 RVA: 0x0000725F File Offset: 0x0000545F
		internal void PublishDebug()
		{
			this.Publish(true, true);
		}

		// Token: 0x06000148 RID: 328 RVA: 0x00007269 File Offset: 0x00005469
		internal void Publish(bool isValidate)
		{
			this.Publish(isValidate, false);
		}

		// Token: 0x06000149 RID: 329 RVA: 0x00007274 File Offset: 0x00005474
		internal void Publish(bool isValidate, bool isDebugChannel)
		{
			if (DatabaseFailureItem.publishDatabaseFailureItemTestHook.Value != null)
			{
				DatabaseFailureItem.publishDatabaseFailureItemTestHook.Value();
			}
			if (isValidate)
			{
				this.Validate();
			}
			IntPtr intPtr = IntPtr.Zero;
			IntPtr intPtr2 = IntPtr.Zero;
			IntPtr intPtr3 = IntPtr.Zero;
			ExDbFailureItemApi.HaDbFailureItem haDbFailureItem = default(ExDbFailureItemApi.HaDbFailureItem);
			try
			{
				haDbFailureItem.CbSize = Marshal.SizeOf(typeof(ExDbFailureItemApi.HaDbFailureItem));
				haDbFailureItem.NameSpace = this.NameSpace;
				haDbFailureItem.Tag = this.Tag;
				haDbFailureItem.Guid = this.Guid;
				if (this.InstanceName != null)
				{
					haDbFailureItem.InstanceName = this.InstanceName;
				}
				if (this.ComponentName != null)
				{
					haDbFailureItem.ComponentName = this.ComponentName;
				}
				if (this.IoError != null)
				{
					ExDbFailureItemApi.HaDbIoErrorInfo haDbIoErrorInfo = default(ExDbFailureItemApi.HaDbIoErrorInfo);
					haDbIoErrorInfo.CbSize = Marshal.SizeOf(typeof(ExDbFailureItemApi.HaDbIoErrorInfo));
					haDbIoErrorInfo.Category = this.IoError.Category;
					if (this.IoError.FileName != null)
					{
						haDbIoErrorInfo.FileName = this.IoError.FileName;
					}
					haDbIoErrorInfo.Offset = this.IoError.Offset;
					haDbIoErrorInfo.Size = this.IoError.Size;
					intPtr = Marshal.AllocHGlobal(haDbIoErrorInfo.CbSize);
					Marshal.StructureToPtr(haDbIoErrorInfo, intPtr, false);
				}
				haDbFailureItem.IoError = intPtr;
				if (this.NotifyEvent != null)
				{
					ExDbFailureItemApi.HaDbNotificationEventInfo haDbNotificationEventInfo = default(ExDbFailureItemApi.HaDbNotificationEventInfo);
					haDbNotificationEventInfo.CbSize = Marshal.SizeOf(typeof(ExDbFailureItemApi.HaDbNotificationEventInfo));
					haDbNotificationEventInfo.EventId = this.NotifyEvent.EventId;
					if (this.NotifyEvent.Parameters != null)
					{
						haDbNotificationEventInfo.NumParameters = this.NotifyEvent.Parameters.Length;
						intPtr2 = MarshalHelper.StringArrayToIntPtr(this.NotifyEvent.Parameters);
						haDbNotificationEventInfo.Parameters = intPtr2;
					}
					intPtr3 = Marshal.AllocHGlobal(haDbNotificationEventInfo.CbSize);
					Marshal.StructureToPtr(haDbNotificationEventInfo, intPtr3, false);
				}
				haDbFailureItem.NotificationEventInfo = intPtr3;
				if (this.Message != null)
				{
					haDbFailureItem.Message = this.Message;
				}
				ExTraceGlobals.FaultInjectionTracer.TraceTest(2837851453U);
				int num = ExDbFailureItemApi.PublishFailureItemEx(isDebugChannel, ref haDbFailureItem);
				if (num != 0)
				{
					throw new ExDbApiException(new Win32Exception(num));
				}
			}
			finally
			{
				Marshal.FreeHGlobal(intPtr);
				intPtr = IntPtr.Zero;
				if (intPtr2 != IntPtr.Zero)
				{
					MarshalHelper.FreeIntPtrOfMarshalledObjectsArray(intPtr2, this.NotifyEvent.Parameters.Length);
					intPtr2 = IntPtr.Zero;
				}
				Marshal.FreeHGlobal(intPtr3);
				intPtr3 = IntPtr.Zero;
			}
		}

		// Token: 0x0600014A RID: 330 RVA: 0x000074FC File Offset: 0x000056FC
		internal void Validate()
		{
			if (!Enum.IsDefined(this.NameSpace.GetType(), this.NameSpace))
			{
				throw new InvalidFailureItemException("NameSpace");
			}
			if (!Enum.IsDefined(this.Tag.GetType(), this.Tag))
			{
				throw new InvalidFailureItemException("Tag");
			}
			if (this.Guid == Guid.Empty)
			{
				throw new InvalidFailureItemException("Guid");
			}
			if (this.InstanceName == null)
			{
				throw new InvalidFailureItemException("InstanceName");
			}
			if (this.IoError != null)
			{
				if (!Enum.IsDefined(this.IoError.Category.GetType(), this.IoError.Category))
				{
					throw new InvalidFailureItemException("IoErrorInfo.Category");
				}
				if (this.IoError.FileName == null)
				{
					throw new InvalidFailureItemException("IoErrorInfo.FileName");
				}
			}
			if (this.NotifyEvent != null && this.NotifyEvent.EventId == -1)
			{
				throw new InvalidFailureItemException("NotificationEventInfo.EventId");
			}
		}

		// Token: 0x04000157 RID: 343
		internal static readonly int ApiVersion = 1;

		// Token: 0x04000158 RID: 344
		internal static readonly string ChannelName = "Microsoft-Exchange-MailboxDatabaseFailureItems/Operational";

		// Token: 0x04000159 RID: 345
		internal static readonly string DebugChannelName = "Microsoft-Exchange-MailboxDatabaseFailureItems/Debug";

		// Token: 0x0400015A RID: 346
		internal static readonly Guid ChannelGuid = new Guid("{08E893EA-4C11-4fff-A737-99B9EEDEE4F4}");

		// Token: 0x0400015B RID: 347
		internal static readonly string DefaultQueryString = "*[UserData/EventXML]";

		// Token: 0x0400015C RID: 348
		private static Hookable<Action> publishDatabaseFailureItemTestHook = Hookable<Action>.Create(true, null);

		// Token: 0x0400015D RID: 349
		private FailureNameSpace nameSpace;

		// Token: 0x0400015E RID: 350
		private FailureTag tag;

		// Token: 0x0400015F RID: 351
		private Guid guid;

		// Token: 0x04000160 RID: 352
		private string instanceName;

		// Token: 0x04000161 RID: 353
		private string componentName;

		// Token: 0x04000162 RID: 354
		private IoErrorInfo ioError;

		// Token: 0x04000163 RID: 355
		private NotificationEventInfo notifyEvent;

		// Token: 0x04000164 RID: 356
		private DateTime creationTime;

		// Token: 0x04000165 RID: 357
		private EventBookmark bookMark;

		// Token: 0x04000166 RID: 358
		private string message;

		// Token: 0x02000040 RID: 64
		internal class PropertyContainer
		{
			// Token: 0x0600014C RID: 332 RVA: 0x00007658 File Offset: 0x00005858
			internal PropertyContainer(IList<EventProperty> props)
			{
				this.properties = props;
				this.propertiesCount = props.Count;
			}

			// Token: 0x0600014D RID: 333 RVA: 0x00007674 File Offset: 0x00005874
			internal T Get<T>(EventRecordParameteIndex index)
			{
				if (index > (EventRecordParameteIndex)(this.propertiesCount - 1))
				{
					throw new InvalidFailureItemException(string.Format("index {0} is out of range (max={1})", (int)index, this.propertiesCount));
				}
				EventProperty eventProperty = this.properties[(int)index];
				if (eventProperty == null)
				{
					throw new InvalidFailureItemException(string.Format("property# {0} is null", index));
				}
				object value = eventProperty.Value;
				if (value == null)
				{
					throw new InvalidFailureItemException(string.Format("property value# {0} is null", index));
				}
				return (T)((object)value);
			}

			// Token: 0x0600014E RID: 334 RVA: 0x000076FC File Offset: 0x000058FC
			internal T GetEnum<T>(EventRecordParameteIndex index)
			{
				object obj = this.Get<object>(index);
				object obj2;
				if (obj is uint)
				{
					obj2 = (uint)obj;
				}
				else
				{
					obj2 = (int)obj;
				}
				return (T)((object)obj2);
			}

			// Token: 0x0600014F RID: 335 RVA: 0x00007739 File Offset: 0x00005939
			internal bool IsIndexValid(EventRecordParameteIndex index)
			{
				return index <= (EventRecordParameteIndex)(this.propertiesCount - 1);
			}

			// Token: 0x04000167 RID: 359
			private readonly int propertiesCount;

			// Token: 0x04000168 RID: 360
			private readonly IList<EventProperty> properties;
		}
	}
}
