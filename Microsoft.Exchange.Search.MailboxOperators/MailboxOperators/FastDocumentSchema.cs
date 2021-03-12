using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading;
using Microsoft.Ceres.Evaluation.DataModel.Types.BuiltIn;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.Search;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Search.Core.Abstraction;
using Microsoft.Exchange.Search.Core.Common;
using Microsoft.Exchange.Search.OperatorSchema;
using Microsoft.Exchange.Search.TokenOperators;

namespace Microsoft.Exchange.Search.MailboxOperators
{
	// Token: 0x0200000C RID: 12
	internal class FastDocumentSchema
	{
		// Token: 0x06000033 RID: 51 RVA: 0x0000280C File Offset: 0x00000A0C
		private FastDocumentSchema()
		{
			this.DefaultProperties = new List<PropertyDefinition>();
			this.AllProperties = new List<FastPropertyDefinition>();
			this.ItemProperties = new List<FastPropertyDefinition>();
			this.ContactProperties = new List<FastPropertyDefinition>();
			this.MeetingProperties = new List<FastPropertyDefinition>();
			this.MessageProperties = new List<FastPropertyDefinition>();
			this.TaskProperties = new List<FastPropertyDefinition>();
			this.CalendarProperties = new List<FastPropertyDefinition>();
			this.SearchableCalendarProperties = new List<FastPropertyDefinition>();
			this.SearchableContactProperties = new List<FastPropertyDefinition>();
			this.SearchableItemProperties = new List<FastPropertyDefinition>();
			this.SearchableMeetingProperties = new List<FastPropertyDefinition>();
			this.SearchableMessageProperties = new List<FastPropertyDefinition>();
			this.SearchableTaskProperties = new List<FastPropertyDefinition>();
			BindingFlags bindingAttr = BindingFlags.Static | BindingFlags.Public;
			FieldInfo[] fields = base.GetType().GetFields(bindingAttr);
			foreach (FieldInfo fieldInfo in fields)
			{
				FastDocumentSchema.FastPropertyDefinitionAttribute[] array2 = fieldInfo.GetCustomAttributes(typeof(FastDocumentSchema.FastPropertyDefinitionAttribute), false) as FastDocumentSchema.FastPropertyDefinitionAttribute[];
				if (array2 != null && array2.Length > 0)
				{
					FastDocumentSchema.FastPropertyDefinitionAttribute[] array3 = array2;
					int num = 0;
					if (num < array3.Length)
					{
						FastDocumentSchema.FastPropertyDefinitionAttribute fastPropertyDefinitionAttribute = array3[num];
						FastPropertyDefinition fastPropertyDefinition = fieldInfo.GetValue(null) as FastPropertyDefinition;
						if (fastPropertyDefinition != null)
						{
							this.AllProperties.Add(fastPropertyDefinition);
							foreach (StorePropertyDefinition item in fastPropertyDefinition.SourcePropertyDefinitions)
							{
								this.DefaultProperties.Add(item);
							}
							if ((fastPropertyDefinitionAttribute.Types & FastDocumentSchema.PropertyDefinitionItemTypes.Item) == FastDocumentSchema.PropertyDefinitionItemTypes.Item)
							{
								this.ItemProperties.Add(fastPropertyDefinition);
								FastDocumentSchema.AddToCollectionIfSearchable(fastPropertyDefinition, this.SearchableItemProperties);
							}
							if ((fastPropertyDefinitionAttribute.Types & FastDocumentSchema.PropertyDefinitionItemTypes.Contact) == FastDocumentSchema.PropertyDefinitionItemTypes.Contact)
							{
								this.ContactProperties.Add(fastPropertyDefinition);
								FastDocumentSchema.AddToCollectionIfSearchable(fastPropertyDefinition, this.SearchableContactProperties);
							}
							if ((fastPropertyDefinitionAttribute.Types & FastDocumentSchema.PropertyDefinitionItemTypes.Meeting) == FastDocumentSchema.PropertyDefinitionItemTypes.Meeting)
							{
								this.MeetingProperties.Add(fastPropertyDefinition);
								FastDocumentSchema.AddToCollectionIfSearchable(fastPropertyDefinition, this.SearchableMeetingProperties);
							}
							if ((fastPropertyDefinitionAttribute.Types & FastDocumentSchema.PropertyDefinitionItemTypes.Message) == FastDocumentSchema.PropertyDefinitionItemTypes.Message)
							{
								this.MessageProperties.Add(fastPropertyDefinition);
								FastDocumentSchema.AddToCollectionIfSearchable(fastPropertyDefinition, this.SearchableMessageProperties);
							}
							if ((fastPropertyDefinitionAttribute.Types & FastDocumentSchema.PropertyDefinitionItemTypes.Task) == FastDocumentSchema.PropertyDefinitionItemTypes.Task)
							{
								this.TaskProperties.Add(fastPropertyDefinition);
								FastDocumentSchema.AddToCollectionIfSearchable(fastPropertyDefinition, this.SearchableTaskProperties);
							}
							if ((fastPropertyDefinitionAttribute.Types & FastDocumentSchema.PropertyDefinitionItemTypes.Calendar) == FastDocumentSchema.PropertyDefinitionItemTypes.Calendar)
							{
								this.CalendarProperties.Add(fastPropertyDefinition);
								FastDocumentSchema.AddToCollectionIfSearchable(fastPropertyDefinition, this.SearchableCalendarProperties);
							}
						}
					}
				}
			}
			FastDocumentSchema.workingSetFlags = Enum.GetValues(typeof(WorkingSetFlags));
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000034 RID: 52 RVA: 0x00002A88 File Offset: 0x00000C88
		public static FastDocumentSchema Instance
		{
			get
			{
				if (Interlocked.CompareExchange<FastDocumentSchema>(ref FastDocumentSchema.instance, null, null) == null)
				{
					lock (FastDocumentSchema.lockObject)
					{
						if (FastDocumentSchema.instance == null)
						{
							FastDocumentSchema fastDocumentSchema = new FastDocumentSchema();
							Thread.MemoryBarrier();
							FastDocumentSchema.instance = fastDocumentSchema;
						}
					}
				}
				return FastDocumentSchema.instance;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000035 RID: 53 RVA: 0x00002AEC File Offset: 0x00000CEC
		// (set) Token: 0x06000036 RID: 54 RVA: 0x00002AF4 File Offset: 0x00000CF4
		public List<PropertyDefinition> DefaultProperties { get; private set; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000037 RID: 55 RVA: 0x00002AFD File Offset: 0x00000CFD
		// (set) Token: 0x06000038 RID: 56 RVA: 0x00002B05 File Offset: 0x00000D05
		public List<FastPropertyDefinition> AllProperties { get; private set; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000039 RID: 57 RVA: 0x00002B0E File Offset: 0x00000D0E
		// (set) Token: 0x0600003A RID: 58 RVA: 0x00002B16 File Offset: 0x00000D16
		public List<FastPropertyDefinition> ItemProperties { get; private set; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600003B RID: 59 RVA: 0x00002B1F File Offset: 0x00000D1F
		// (set) Token: 0x0600003C RID: 60 RVA: 0x00002B27 File Offset: 0x00000D27
		public List<FastPropertyDefinition> ContactProperties { get; private set; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600003D RID: 61 RVA: 0x00002B30 File Offset: 0x00000D30
		// (set) Token: 0x0600003E RID: 62 RVA: 0x00002B38 File Offset: 0x00000D38
		public List<FastPropertyDefinition> MeetingProperties { get; private set; }

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600003F RID: 63 RVA: 0x00002B41 File Offset: 0x00000D41
		// (set) Token: 0x06000040 RID: 64 RVA: 0x00002B49 File Offset: 0x00000D49
		public List<FastPropertyDefinition> MessageProperties { get; private set; }

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000041 RID: 65 RVA: 0x00002B52 File Offset: 0x00000D52
		// (set) Token: 0x06000042 RID: 66 RVA: 0x00002B5A File Offset: 0x00000D5A
		public List<FastPropertyDefinition> TaskProperties { get; private set; }

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000043 RID: 67 RVA: 0x00002B63 File Offset: 0x00000D63
		// (set) Token: 0x06000044 RID: 68 RVA: 0x00002B6B File Offset: 0x00000D6B
		public List<FastPropertyDefinition> CalendarProperties { get; private set; }

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000045 RID: 69 RVA: 0x00002B74 File Offset: 0x00000D74
		// (set) Token: 0x06000046 RID: 70 RVA: 0x00002B7C File Offset: 0x00000D7C
		public List<FastPropertyDefinition> SearchableItemProperties { get; private set; }

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000047 RID: 71 RVA: 0x00002B85 File Offset: 0x00000D85
		// (set) Token: 0x06000048 RID: 72 RVA: 0x00002B8D File Offset: 0x00000D8D
		public List<FastPropertyDefinition> SearchableMeetingProperties { get; private set; }

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000049 RID: 73 RVA: 0x00002B96 File Offset: 0x00000D96
		// (set) Token: 0x0600004A RID: 74 RVA: 0x00002B9E File Offset: 0x00000D9E
		public List<FastPropertyDefinition> SearchableCalendarProperties { get; private set; }

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600004B RID: 75 RVA: 0x00002BA7 File Offset: 0x00000DA7
		// (set) Token: 0x0600004C RID: 76 RVA: 0x00002BAF File Offset: 0x00000DAF
		public List<FastPropertyDefinition> SearchableContactProperties { get; private set; }

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600004D RID: 77 RVA: 0x00002BB8 File Offset: 0x00000DB8
		// (set) Token: 0x0600004E RID: 78 RVA: 0x00002BC0 File Offset: 0x00000DC0
		public List<FastPropertyDefinition> SearchableTaskProperties { get; private set; }

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600004F RID: 79 RVA: 0x00002BC9 File Offset: 0x00000DC9
		// (set) Token: 0x06000050 RID: 80 RVA: 0x00002BD1 File Offset: 0x00000DD1
		public List<FastPropertyDefinition> SearchableMessageProperties { get; private set; }

		// Token: 0x06000051 RID: 81 RVA: 0x00003158 File Offset: 0x00001358
		public IEnumerable<IRetrieverPropertyDefinition> GetEmbeddedMessageProperties(Item item)
		{
			foreach (FastPropertyDefinition property in FastDocumentSchema.Instance.SearchableItemProperties)
			{
				yield return property;
			}
			if (item is MeetingMessage)
			{
				foreach (FastPropertyDefinition property2 in FastDocumentSchema.Instance.SearchableMeetingProperties)
				{
					yield return property2;
				}
			}
			if (item is CalendarItem)
			{
				foreach (FastPropertyDefinition property3 in FastDocumentSchema.Instance.SearchableCalendarProperties)
				{
					yield return property3;
				}
			}
			if (item is Contact || item is DistributionList)
			{
				foreach (FastPropertyDefinition property4 in FastDocumentSchema.Instance.SearchableContactProperties)
				{
					yield return property4;
				}
			}
			if (item is Task)
			{
				foreach (FastPropertyDefinition property5 in FastDocumentSchema.Instance.SearchableTaskProperties)
				{
					yield return property5;
				}
			}
			if (item is MessageItem)
			{
				foreach (FastPropertyDefinition property6 in FastDocumentSchema.Instance.SearchableMessageProperties)
				{
					yield return property6;
				}
			}
			yield break;
		}

		// Token: 0x06000052 RID: 82 RVA: 0x0000317C File Offset: 0x0000137C
		private static List<string> ExtractRecipients(Item item, RecipientItemType type)
		{
			List<string> list = new List<string>();
			MessageItem messageItem = item as MessageItem;
			if (messageItem != null)
			{
				foreach (Recipient recipient in messageItem.Recipients)
				{
					if (recipient.RecipientItemType == type)
					{
						string text = SearchParticipant.ToParticipantString(recipient.Participant);
						if (!string.IsNullOrEmpty(text))
						{
							list.Add(text);
						}
					}
				}
			}
			return list;
		}

		// Token: 0x06000053 RID: 83 RVA: 0x000031FC File Offset: 0x000013FC
		private static List<string> ExtractGroupExpansionRecipients(Item item, StorePropertyDefinition propertyDefinition, RecipientItemType? recipientType)
		{
			List<string> list = null;
			MessageItem messageItem = item as MessageItem;
			if (messageItem != null)
			{
				GroupExpansionRecipients groupExpansionRecipients = Microsoft.Exchange.Data.Storage.GroupExpansionRecipients.RetrieveFromStore(messageItem, propertyDefinition);
				if (groupExpansionRecipients != null && groupExpansionRecipients.Recipients.Count > 0)
				{
					list = new List<string>(groupExpansionRecipients.Recipients.Count);
					foreach (RecipientToIndex recipientToIndex in groupExpansionRecipients.Recipients)
					{
						if (recipientType == null || recipientType == null || recipientType.Value == recipientToIndex.RecipientType)
						{
							string item2 = SearchParticipant.ToParticipantString(recipientToIndex.EmailAddress, recipientToIndex.DisplayName);
							list.Add(item2);
						}
					}
				}
			}
			return list;
		}

		// Token: 0x06000054 RID: 84 RVA: 0x000032C4 File Offset: 0x000014C4
		private static List<string> GetWorkingSetIndexFlags(WorkingSetFlags workingSetStoreFlags)
		{
			List<string> list = new List<string>();
			foreach (object obj in FastDocumentSchema.workingSetFlags)
			{
				WorkingSetFlags workingSetFlags = (WorkingSetFlags)obj;
				if ((workingSetStoreFlags & workingSetFlags) == workingSetFlags)
				{
					list.Add(workingSetFlags.ToString());
				}
			}
			return list;
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00003334 File Offset: 0x00001534
		private static List<string> GetPropertyValues(Item item, params StorePropertyDefinition[] props)
		{
			List<string> list = new List<string>();
			foreach (StorePropertyDefinition propertyDefinition in props)
			{
				string text = item.TryGetProperty(propertyDefinition) as string;
				if (text != null)
				{
					list.Add(text);
				}
			}
			return list;
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00003378 File Offset: 0x00001578
		private static StringBuilder GetStringBuilder(int initialSize)
		{
			StringBuilder stringBuilder = null;
			lock (FastDocumentSchema.stringBuilderPoolList)
			{
				if (FastDocumentSchema.stringBuilderPoolList.Count > 0)
				{
					foreach (StringBuilder stringBuilder2 in FastDocumentSchema.stringBuilderPoolList)
					{
						if (stringBuilder2.Capacity >= initialSize)
						{
							stringBuilder = stringBuilder2;
							break;
						}
						if (stringBuilder == null || stringBuilder.Capacity < stringBuilder2.Capacity)
						{
							stringBuilder = stringBuilder2;
						}
					}
					if (stringBuilder != null)
					{
						FastDocumentSchema.stringBuilderPoolList.Remove(stringBuilder);
					}
				}
			}
			if (stringBuilder == null)
			{
				stringBuilder = new StringBuilder(initialSize);
			}
			stringBuilder.Clear();
			return stringBuilder;
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00003468 File Offset: 0x00001668
		private static void ReturnStringBuilder(StringBuilder stringBuilder)
		{
			if (stringBuilder != null)
			{
				lock (FastDocumentSchema.stringBuilderPoolList)
				{
					if (!FastDocumentSchema.stringBuilderPoolList.Contains(stringBuilder) && FastDocumentSchema.stringBuilderPoolList.Count > 100)
					{
						FastDocumentSchema.stringBuilderPoolList.Add(stringBuilder);
						FastDocumentSchema.stringBuilderPoolList.Sort(delegate(StringBuilder first, StringBuilder second)
						{
							if (second == null)
							{
								return 1;
							}
							if (first == null)
							{
								return -1;
							}
							return first.Capacity.CompareTo(second.Capacity);
						});
					}
				}
			}
		}

		// Token: 0x06000058 RID: 88 RVA: 0x000034F4 File Offset: 0x000016F4
		private static char[] GetCopyBuffer()
		{
			char[] array = null;
			lock (FastDocumentSchema.charBuffers)
			{
				if (FastDocumentSchema.charBuffers.Count > 0)
				{
					array = FastDocumentSchema.charBuffers[0];
					FastDocumentSchema.charBuffers.RemoveAt(0);
				}
			}
			if (array == null)
			{
				array = new char[16384];
			}
			else
			{
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = '\0';
				}
			}
			return array;
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00003578 File Offset: 0x00001778
		private static void ReleaseCharBuffer(char[] buffer)
		{
			if (buffer != null && buffer.Length != 16384)
			{
				lock (FastDocumentSchema.charBuffers)
				{
					if (FastDocumentSchema.charBuffers.Count < 100 && !FastDocumentSchema.charBuffers.Contains(buffer))
					{
						FastDocumentSchema.charBuffers.Add(buffer);
					}
				}
			}
		}

		// Token: 0x0600005A RID: 90 RVA: 0x000035E4 File Offset: 0x000017E4
		private static void AddToCollectionIfSearchable(FastPropertyDefinition propertyDefinition, List<FastPropertyDefinition> searchablePropertyDefinitionList)
		{
			if (propertyDefinition.FieldDefinition != null && propertyDefinition.FieldDefinition.Definition.Searchable && !searchablePropertyDefinitionList.Contains(propertyDefinition))
			{
				searchablePropertyDefinitionList.Add(propertyDefinition);
			}
		}

		// Token: 0x04000009 RID: 9
		private const int CharBufferSize = 16384;

		// Token: 0x0400000A RID: 10
		private const int MaxItemsInPool = 100;

		// Token: 0x0400000B RID: 11
		private const int PreviewLength = 60;

		// Token: 0x0400000C RID: 12
		[FastDocumentSchema.FastPropertyDefinitionAttribute(FastDocumentSchema.PropertyDefinitionItemTypes.Item)]
		public static readonly FastPropertyDefinition AnnotationToken = new FastPropertyDefinition("annotationtoken", 7, PropertyDefinitionAttribute.None, new StorePropertyDefinition[]
		{
			ItemSchema.AnnotationToken
		}, delegate(Item item, RetrieverProducer producer)
		{
			IUpdateableInt32Field updateableInt32Field = (IUpdateableInt32Field)producer.Holder[producer.ErrorCodeIndex];
			EvaluationErrors evaluationErrors = (EvaluationErrors)(updateableInt32Field.IsNull() ? 0 : updateableInt32Field.Int32Value);
			if (evaluationErrors == EvaluationErrors.AnnotationTokenError)
			{
				ExTraceGlobals.AnnotationTokenTracer.TraceDebug(0L, "Skip annotation token as this message had annotation error before.");
				updateableInt32Field.Int32Value = 0;
				return false;
			}
			MessageItem messageItem = item as MessageItem;
			if (messageItem != null && (messageItem.IsRestricted || ObjectClass.IsSmime(messageItem.ClassName)))
			{
				ExTraceGlobals.AnnotationTokenTracer.TraceDebug(0L, "Skip annotation token since this message is Rights Managed.");
				return false;
			}
			return true;
		}, delegate(Item item)
		{
			object obj = item.TryGetProperty(ItemSchema.AnnotationToken);
			if (PropertyError.IsPropertyError(obj))
			{
				if (PropertyError.IsPropertyValueTooBig(obj))
				{
					using (Stream stream = item.OpenPropertyStream(ItemSchema.AnnotationToken, PropertyOpenMode.ReadOnly))
					{
						byte[] array = new byte[TokenInfo.Version.Length];
						int num = stream.Read(array, 0, TokenInfo.Version.Length);
						if (num != TokenInfo.Version.Length)
						{
							ExTraceGlobals.AnnotationTokenTracer.TraceDebug(0L, "Ignore invalid annotation token blob.");
							return new byte[0];
						}
						if (!TokenInfo.IsVersionSupported(array))
						{
							ExTraceGlobals.AnnotationTokenTracer.TraceDebug(0L, "Version of the annotation token blob doesn't match.");
							return new byte[0];
						}
						stream.Seek(0L, SeekOrigin.Begin);
						using (MemoryStream memoryStream = new MemoryStream())
						{
							stream.CopyTo(memoryStream);
							return memoryStream.ToArray();
						}
					}
				}
				ExTraceGlobals.AnnotationTokenTracer.TraceDebug(0L, "Failed to get annotation token property. Error: {0}.", new object[]
				{
					obj
				});
				return new byte[0];
			}
			byte[] array2 = (byte[])obj;
			if (array2.Length <= TokenInfo.Version.Length)
			{
				ExTraceGlobals.AnnotationTokenTracer.TraceDebug(0L, "Ignore invalid annotation token blob.");
				return new byte[0];
			}
			if (!TokenInfo.IsVersionSupported(array2))
			{
				ExTraceGlobals.AnnotationTokenTracer.TraceDebug(0L, "Version of the annotation token blob doesn't match.");
				return new byte[0];
			}
			return array2;
		}, delegate(RetrieverProducer producer, object value)
		{
			byte[] array = (byte[])value;
			if (array.Length != 0)
			{
				((IUpdateableBlobField)producer.Holder[producer.AnnotationTokenIndex]).BlobValue = array;
				producer.IncrementPerfcounterBy(OperatorPerformanceCounter.TotalAnnotationTokenBytes, (long)array.Length);
				producer.IncrementPerfcounter(OperatorPerformanceCounter.RetrieverNumberOfItemsWithValidAnnotationToken);
				return;
			}
			producer.IncrementPerfcounter(OperatorPerformanceCounter.RetrieverNumberOfItemsWithoutAnnotationToken);
		});

		// Token: 0x0400000D RID: 13
		[FastDocumentSchema.FastPropertyDefinitionAttribute(FastDocumentSchema.PropertyDefinitionItemTypes.Item)]
		public static readonly FastPropertyDefinition DetectedLanguage = new FastPropertyDefinition("detectedlanguage", 1, PropertyDefinitionAttribute.None, new StorePropertyDefinition[]
		{
			ItemSchema.DetectedLanguage
		}, null, (Item item) => item.GetValueOrDefault<string>(ItemSchema.DetectedLanguage, string.Empty), delegate(RetrieverProducer producer, object value)
		{
			((IUpdateableStringField)producer.Holder[producer.DetectedLanguageIndex]).StringValue = (string)value;
			if (string.IsNullOrEmpty((string)value))
			{
				IBlobField blobField = (IBlobField)producer.Holder[producer.AnnotationTokenIndex];
				byte[] blobValue = blobField.BlobValue;
				if (blobValue != null && blobValue.Length > 0)
				{
					TokenInfo tokenInfo = null;
					using (MemoryStream memoryStream = new MemoryStream(blobValue))
					{
						try
						{
							tokenInfo = TokenInfo.Create(memoryStream);
						}
						catch (Exception arg)
						{
							ExTraceGlobals.AnnotationTokenTracer.TraceDebug<Exception>(0L, "Failed to create TokenInfo object from the annotation blob. Exception = {0}.", arg);
						}
					}
					if (tokenInfo != null)
					{
						((IUpdateableStringField)producer.Holder[producer.TempBodyIndex]).StringValue = tokenInfo.Text;
						return;
					}
					producer.Holder[producer.TempBodyIndex] = producer.Holder[producer.BodyIndex];
					((IUpdateableBlobField)producer.Holder[producer.AnnotationTokenIndex]).BlobValue = null;
					return;
				}
				else
				{
					producer.Holder[producer.TempBodyIndex] = producer.Holder[producer.BodyIndex];
				}
			}
		});

		// Token: 0x0400000E RID: 14
		[FastDocumentSchema.FastPropertyDefinitionAttribute(FastDocumentSchema.PropertyDefinitionItemTypes.Item)]
		public static readonly FastPropertyDefinition Body = new FastPropertyDefinition(FastIndexSystemSchema.Body, PropertyDefinitionAttribute.PartOfAttachmentAnnotation | PropertyDefinitionAttribute.SkipValueTracing, null, delegate(Item item, RetrieverProducer producer)
		{
			bool flag = ((IBlobField)producer.Holder[producer.AnnotationTokenIndex]).BlobValue != null;
			ExTraceGlobals.AnnotationTokenTracer.TraceDebug<string, string>(0L, "Annotation token is {0}present{1}.", flag ? string.Empty : "not ", flag ? ", skip loading body" : string.Empty);
			return !flag;
		}, delegate(Item item)
		{
			RightsManagedMessageItem rightsManagedMessageItem = item as RightsManagedMessageItem;
			Body body;
			if (rightsManagedMessageItem != null && rightsManagedMessageItem.IsRestricted && rightsManagedMessageItem.IsDecoded)
			{
				body = rightsManagedMessageItem.ProtectedBody;
			}
			else
			{
				body = item.Body;
			}
			string result = string.Empty;
			try
			{
				StringBuilder stringBuilder = null;
				char[] array = null;
				try
				{
					stringBuilder = FastDocumentSchema.GetStringBuilder((int)body.Size);
					array = FastDocumentSchema.GetCopyBuffer();
					using (TextReader textReader = body.OpenTextReader(BodyFormat.TextPlain))
					{
						for (int i = textReader.Read(array, 0, array.Length); i > 0; i = textReader.Read(array, 0, array.Length))
						{
							stringBuilder.Append(array, 0, i);
						}
					}
					result = stringBuilder.ToString();
				}
				finally
				{
					FastDocumentSchema.ReleaseCharBuffer(array);
					FastDocumentSchema.ReturnStringBuilder(stringBuilder);
				}
			}
			catch (ConversionFailedException result2)
			{
				return result2;
			}
			return result;
		}, delegate(RetrieverProducer producer, object value)
		{
			Exception ex;
			try
			{
				ex = (value as ConversionFailedException);
				if (ex == null)
				{
					string text = ((string)value) ?? string.Empty;
					((IUpdateableStringField)producer.Holder[producer.BodyIndex]).StringValue = text;
					((IUpdateableStringField)producer.Holder[producer.FileTypeIndex]).StringValue = "txt";
					ExTraceGlobals.RetrieverOperatorTracer.TracePerformance<int>(0L, "Message body length:                     {0}", text.Length);
					producer.IncrementPerfcounterBy(OperatorPerformanceCounter.TotalBodyChars, (long)text.Length);
				}
			}
			catch (ArgumentException ex2)
			{
				ex = ex2;
			}
			if (ex != null)
			{
				ManagedProperties.SetAsPartiallyProcessed(producer.Holder);
				IUpdateableStringField updateableStringField = (IUpdateableStringField)producer.Holder[producer.ErrorMessageIndex];
				string stringValue = string.IsNullOrEmpty(updateableStringField.StringValue) ? ex.ToString() : string.Format("{0} {1}", updateableStringField.StringValue, ex);
				updateableStringField.StringValue = stringValue;
			}
		});

		// Token: 0x0400000F RID: 15
		[FastDocumentSchema.FastPropertyDefinitionAttribute(FastDocumentSchema.PropertyDefinitionItemTypes.Item)]
		public static readonly FastPropertyDefinition Subject = new FastPropertyDefinition(FastIndexSystemSchema.Subject, PropertyDefinitionAttribute.PartOfAttachmentAnnotation, ItemSchema.Subject, null, delegate(RetrieverProducer producer, object value)
		{
			((IUpdateableStringField)producer.Holder[producer.SubjectIndex]).StringValue = (((string)value) ?? string.Empty);
		});

		// Token: 0x04000010 RID: 16
		[FastDocumentSchema.FastPropertyDefinitionAttribute(FastDocumentSchema.PropertyDefinitionItemTypes.Meeting | FastDocumentSchema.PropertyDefinitionItemTypes.Calendar)]
		public static readonly FastPropertyDefinition MeetingLocation = new FastPropertyDefinition(FastIndexSystemSchema.MeetingLocation, PropertyDefinitionAttribute.None, delegate(Item item)
		{
			CalendarItem calendarItem = item as CalendarItem;
			if (calendarItem != null)
			{
				return calendarItem.Location;
			}
			MeetingRequest meetingRequest = item as MeetingRequest;
			if (meetingRequest != null)
			{
				return meetingRequest.Location;
			}
			return null;
		}, delegate(RetrieverProducer producer, object value)
		{
			((IUpdateableStringField)producer.Holder[producer.MeetingLocationIndex]).StringValue = (((string)value) ?? string.Empty);
		});

		// Token: 0x04000011 RID: 17
		[FastDocumentSchema.FastPropertyDefinitionAttribute(FastDocumentSchema.PropertyDefinitionItemTypes.Message)]
		public static readonly FastPropertyDefinition To = new FastPropertyDefinition(FastIndexSystemSchema.To, PropertyDefinitionAttribute.None, (Item item) => FastDocumentSchema.ExtractRecipients(item, RecipientItemType.To), delegate(RetrieverProducer producer, object value)
		{
			((IUpdateableListField<string>)producer.Holder[producer.ToIndex]).List = (List<string>)value;
		});

		// Token: 0x04000012 RID: 18
		[FastDocumentSchema.FastPropertyDefinitionAttribute(FastDocumentSchema.PropertyDefinitionItemTypes.Message)]
		public static readonly FastPropertyDefinition Cc = new FastPropertyDefinition(FastIndexSystemSchema.Cc, PropertyDefinitionAttribute.None, (Item item) => FastDocumentSchema.ExtractRecipients(item, RecipientItemType.Cc), delegate(RetrieverProducer producer, object value)
		{
			((IUpdateableListField<string>)producer.Holder[producer.CcIndex]).List = (List<string>)value;
		});

		// Token: 0x04000013 RID: 19
		[FastDocumentSchema.FastPropertyDefinitionAttribute(FastDocumentSchema.PropertyDefinitionItemTypes.Message)]
		public static readonly FastPropertyDefinition Bcc = new FastPropertyDefinition(FastIndexSystemSchema.Bcc, PropertyDefinitionAttribute.None, (Item item) => FastDocumentSchema.ExtractRecipients(item, RecipientItemType.Bcc), delegate(RetrieverProducer producer, object value)
		{
			((IUpdateableListField<string>)producer.Holder[producer.BccIndex]).List = (List<string>)value;
		});

		// Token: 0x04000014 RID: 20
		[FastDocumentSchema.FastPropertyDefinitionAttribute(FastDocumentSchema.PropertyDefinitionItemTypes.Message)]
		public static readonly FastPropertyDefinition ToGroupExpansionRecipients = new FastPropertyDefinition(FastIndexSystemSchema.ToGroupExpansionRecipients, PropertyDefinitionAttribute.None, new StorePropertyDefinition[]
		{
			MessageItemSchema.ToGroupExpansionRecipients
		}, (Item item) => FastDocumentSchema.ExtractGroupExpansionRecipients(item, MessageItemSchema.GroupExpansionRecipients, new RecipientItemType?(RecipientItemType.To)), delegate(RetrieverProducer producer, object value)
		{
			((IUpdateableListField<string>)producer.Holder[producer.ToGroupExpansionRecipientsIndex]).List = (List<string>)value;
		});

		// Token: 0x04000015 RID: 21
		[FastDocumentSchema.FastPropertyDefinitionAttribute(FastDocumentSchema.PropertyDefinitionItemTypes.Message)]
		public static readonly FastPropertyDefinition CcGroupExpansionRecipients = new FastPropertyDefinition(FastIndexSystemSchema.CcGroupExpansionRecipients, PropertyDefinitionAttribute.None, new StorePropertyDefinition[]
		{
			MessageItemSchema.CcGroupExpansionRecipients
		}, (Item item) => FastDocumentSchema.ExtractGroupExpansionRecipients(item, MessageItemSchema.GroupExpansionRecipients, new RecipientItemType?(RecipientItemType.Cc)), delegate(RetrieverProducer producer, object value)
		{
			((IUpdateableListField<string>)producer.Holder[producer.CcGroupExpansionRecipientsIndex]).List = (List<string>)value;
		});

		// Token: 0x04000016 RID: 22
		[FastDocumentSchema.FastPropertyDefinitionAttribute(FastDocumentSchema.PropertyDefinitionItemTypes.Message)]
		public static readonly FastPropertyDefinition BccGroupExpansionRecipients = new FastPropertyDefinition(FastIndexSystemSchema.BccGroupExpansionRecipients, PropertyDefinitionAttribute.None, new StorePropertyDefinition[]
		{
			MessageItemSchema.BccGroupExpansionRecipients
		}, (Item item) => FastDocumentSchema.ExtractGroupExpansionRecipients(item, MessageItemSchema.GroupExpansionRecipients, new RecipientItemType?(RecipientItemType.Bcc)), delegate(RetrieverProducer producer, object value)
		{
			((IUpdateableListField<string>)producer.Holder[producer.BccGroupExpansionRecipientsIndex]).List = (List<string>)value;
		});

		// Token: 0x04000017 RID: 23
		[FastDocumentSchema.FastPropertyDefinitionAttribute(FastDocumentSchema.PropertyDefinitionItemTypes.Message)]
		public static readonly FastPropertyDefinition GroupExpansionRecipients = new FastPropertyDefinition(FastIndexSystemSchema.GroupExpansionRecipients, PropertyDefinitionAttribute.None, new StorePropertyDefinition[]
		{
			MessageItemSchema.GroupExpansionRecipients
		}, (Item item) => FastDocumentSchema.ExtractGroupExpansionRecipients(item, MessageItemSchema.GroupExpansionRecipients, null), delegate(RetrieverProducer producer, object value)
		{
			((IUpdateableListField<string>)producer.Holder[producer.GroupExpansionRecipientsIndex]).List = (List<string>)value;
		});

		// Token: 0x04000018 RID: 24
		[FastDocumentSchema.FastPropertyDefinitionAttribute(FastDocumentSchema.PropertyDefinitionItemTypes.Message | FastDocumentSchema.PropertyDefinitionItemTypes.Calendar)]
		public static readonly FastPropertyDefinition Recipients = new FastPropertyDefinition(FastIndexSystemSchema.Recipients, PropertyDefinitionAttribute.None, delegate(Item item)
		{
			List<string> list = new List<string>();
			IEnumerable<RecipientBase> enumerable = null;
			int capacity = 0;
			MessageItem messageItem = item as MessageItem;
			CalendarItem calendarItem = item as CalendarItem;
			if (messageItem != null)
			{
				enumerable = messageItem.Recipients;
				capacity = messageItem.Recipients.Count;
			}
			else if (calendarItem != null)
			{
				enumerable = calendarItem.AttendeeCollection;
				capacity = calendarItem.AttendeeCollection.Count;
			}
			if (enumerable != null)
			{
				list.Capacity = capacity;
				foreach (RecipientBase recipientBase in enumerable)
				{
					string text = SearchParticipant.ToParticipantString(recipientBase.Participant);
					if (!string.IsNullOrEmpty(text))
					{
						list.Add(text);
					}
				}
			}
			return list;
		}, delegate(RetrieverProducer producer, object value)
		{
			((IUpdateableListField<string>)producer.Holder[producer.MessageRecipientsIndex]).List = (List<string>)value;
		});

		// Token: 0x04000019 RID: 25
		[FastDocumentSchema.FastPropertyDefinitionAttribute(FastDocumentSchema.PropertyDefinitionItemTypes.Message)]
		public static readonly FastPropertyDefinition SharingInfo = new FastPropertyDefinition(FastIndexSystemSchema.SharingInfo, PropertyDefinitionAttribute.None, new StorePropertyDefinition[]
		{
			SharingMessageItemSchema.SharingBrowseUrl,
			SharingMessageItemSchema.SharingLocalName,
			SharingMessageItemSchema.SharingInitiatorName,
			MessageItemSchema.SharingRemoteComment,
			SharingMessageItemSchema.SharingRemotePath,
			SharingMessageItemSchema.SharingRemoteName
		}, (Item item) => FastDocumentSchema.GetPropertyValues(item, new StorePropertyDefinition[]
		{
			SharingMessageItemSchema.SharingBrowseUrl,
			SharingMessageItemSchema.SharingLocalName,
			SharingMessageItemSchema.SharingInitiatorName,
			MessageItemSchema.SharingRemoteComment,
			SharingMessageItemSchema.SharingRemotePath,
			SharingMessageItemSchema.SharingRemoteName
		}), delegate(RetrieverProducer producer, object value)
		{
			((IUpdateableListField<string>)producer.Holder[producer.SharingInfoIndex]).List = (List<string>)value;
		});

		// Token: 0x0400001A RID: 26
		[FastDocumentSchema.FastPropertyDefinitionAttribute(FastDocumentSchema.PropertyDefinitionItemTypes.Item)]
		public static readonly FastPropertyDefinition FolderId = new FastPropertyDefinition(FastIndexSystemSchema.FolderId, PropertyDefinitionAttribute.None, StoreObjectSchema.ParentEntryId, null, delegate(RetrieverProducer producer, object value)
		{
			((IUpdateableStringField)producer.Holder[producer.FolderIdIndex]).StringValue = FolderIdHelper.GetIndexForFolderEntryId((byte[])value);
		});

		// Token: 0x0400001B RID: 27
		[FastDocumentSchema.FastPropertyDefinitionAttribute(FastDocumentSchema.PropertyDefinitionItemTypes.Item)]
		public static readonly FastPropertyDefinition Received = new FastPropertyDefinition(FastIndexSystemSchema.Received, PropertyDefinitionAttribute.None, ItemSchema.ReceivedTime, null, delegate(RetrieverProducer producer, object value)
		{
			((IUpdateableDateTimeField)producer.Holder[producer.ReceivedIndex]).DateTimeValue = Util.NormalizeDateTime((DateTime)((ExDateTime)value));
			((IUpdateableDateTimeField)producer.Holder[producer.RefinableReceivedIndex]).DateTimeValue = Util.NormalizeDateTimeToMinutes((DateTime)((ExDateTime)value));
		});

		// Token: 0x0400001C RID: 28
		[FastDocumentSchema.FastPropertyDefinitionAttribute(FastDocumentSchema.PropertyDefinitionItemTypes.Message)]
		public static readonly FastPropertyDefinition Sent = new FastPropertyDefinition(FastIndexSystemSchema.Sent, PropertyDefinitionAttribute.None, ItemSchema.SentTime, null, delegate(RetrieverProducer producer, object value)
		{
			((IUpdateableDateTimeField)producer.Holder[producer.SentIndex]).DateTimeValue = Util.NormalizeDateTime((DateTime)((ExDateTime)value));
		});

		// Token: 0x0400001D RID: 29
		[FastDocumentSchema.FastPropertyDefinitionAttribute(FastDocumentSchema.PropertyDefinitionItemTypes.Message | FastDocumentSchema.PropertyDefinitionItemTypes.Calendar)]
		public static readonly FastPropertyDefinition From = new FastPropertyDefinition(FastIndexSystemSchema.From, PropertyDefinitionAttribute.None, new StorePropertyDefinition[]
		{
			ItemSchema.Sender,
			ItemSchema.From
		}, delegate(Item item)
		{
			List<string> list = new List<string>(2);
			MessageItem messageItem = item as MessageItem;
			CalendarItem calendarItem = item as CalendarItem;
			Participant participant = null;
			Participant participant2 = null;
			if (messageItem != null)
			{
				participant = messageItem.From;
				participant2 = (item.TryGetProperty(ItemSchema.Sender) as Participant);
			}
			else if (calendarItem != null)
			{
				participant = calendarItem.Organizer;
				participant2 = (calendarItem.TryGetProperty(ItemSchema.Sender) as Participant);
			}
			if (participant != null)
			{
				string text = SearchParticipant.ToParticipantString(participant);
				if (!string.IsNullOrEmpty(text))
				{
					list.Add(text);
				}
			}
			if (participant2 != null)
			{
				string text2 = SearchParticipant.ToParticipantString(participant2);
				if (!string.IsNullOrEmpty(text2) && !list.Contains(text2))
				{
					list.Add(text2);
				}
			}
			return list;
		}, delegate(RetrieverProducer producer, object value)
		{
			List<string> list = (List<string>)value;
			((IUpdateableListField<string>)producer.Holder[producer.FromIndex]).List = list;
			((IUpdateableStringField)producer.Holder[producer.RefinableFromIndex]).StringValue = ((list.Count > 0) ? list[0] : null);
		});

		// Token: 0x0400001E RID: 30
		[FastDocumentSchema.FastPropertyDefinitionAttribute(FastDocumentSchema.PropertyDefinitionItemTypes.Message)]
		public static readonly FastPropertyDefinition ConversationTopic = new FastPropertyDefinition(FastIndexSystemSchema.ConversationTopic, PropertyDefinitionAttribute.None, ItemSchema.ConversationTopic, null, delegate(RetrieverProducer producer, object value)
		{
			((IUpdateableStringField)producer.Holder[producer.ConversationTopicIndex]).StringValue = (((string)value) ?? string.Empty);
		});

		// Token: 0x0400001F RID: 31
		[FastDocumentSchema.FastPropertyDefinitionAttribute(FastDocumentSchema.PropertyDefinitionItemTypes.Contact)]
		public static readonly FastPropertyDefinition Account = new FastPropertyDefinition(FastIndexSystemSchema.Account, PropertyDefinitionAttribute.None, ContactSchema.Account, null, delegate(RetrieverProducer producer, object value)
		{
			((IUpdateableStringField)producer.Holder[producer.AccountIndex]).StringValue = (((string)value) ?? string.Empty);
		});

		// Token: 0x04000020 RID: 32
		[FastDocumentSchema.FastPropertyDefinitionAttribute(FastDocumentSchema.PropertyDefinitionItemTypes.Contact)]
		public static readonly FastPropertyDefinition DisplayName = new FastPropertyDefinition(FastIndexSystemSchema.DisplayName, PropertyDefinitionAttribute.None, ContactBaseSchema.DisplayNameFirstLast, null, delegate(RetrieverProducer producer, object value)
		{
			((IUpdateableStringField)producer.Holder[producer.DisplayNameIndex]).StringValue = (((string)value) ?? string.Empty);
		});

		// Token: 0x04000021 RID: 33
		[FastDocumentSchema.FastPropertyDefinitionAttribute(FastDocumentSchema.PropertyDefinitionItemTypes.Contact)]
		public static readonly FastPropertyDefinition FirstName = new FastPropertyDefinition(FastIndexSystemSchema.FirstName, PropertyDefinitionAttribute.None, ContactSchema.GivenName, null, delegate(RetrieverProducer producer, object value)
		{
			((IUpdateableStringField)producer.Holder[producer.FirstNameIndex]).StringValue = (((string)value) ?? string.Empty);
		});

		// Token: 0x04000022 RID: 34
		[FastDocumentSchema.FastPropertyDefinitionAttribute(FastDocumentSchema.PropertyDefinitionItemTypes.Contact)]
		public static readonly FastPropertyDefinition LastName = new FastPropertyDefinition(FastIndexSystemSchema.LastName, PropertyDefinitionAttribute.None, ContactSchema.Surname, null, delegate(RetrieverProducer producer, object value)
		{
			((IUpdateableStringField)producer.Holder[producer.LastNameIndex]).StringValue = (((string)value) ?? string.Empty);
		});

		// Token: 0x04000023 RID: 35
		[FastDocumentSchema.FastPropertyDefinitionAttribute(FastDocumentSchema.PropertyDefinitionItemTypes.Contact)]
		public static readonly FastPropertyDefinition FileAs = new FastPropertyDefinition(FastIndexSystemSchema.FileAs, PropertyDefinitionAttribute.None, ContactBaseSchema.FileAs, null, delegate(RetrieverProducer producer, object value)
		{
			((IUpdateableStringField)producer.Holder[producer.FileAsIndex]).StringValue = (((string)value) ?? string.Empty);
		});

		// Token: 0x04000024 RID: 36
		[FastDocumentSchema.FastPropertyDefinitionAttribute(FastDocumentSchema.PropertyDefinitionItemTypes.Contact)]
		public static readonly FastPropertyDefinition EmailDisplayName = new FastPropertyDefinition(FastIndexSystemSchema.EmailDisplayName, PropertyDefinitionAttribute.None, new StorePropertyDefinition[]
		{
			ContactSchema.Email1DisplayName,
			ContactSchema.Email2DisplayName,
			ContactSchema.Email3DisplayName
		}, (Item item) => FastDocumentSchema.GetPropertyValues(item, new StorePropertyDefinition[]
		{
			ContactSchema.Email1DisplayName,
			ContactSchema.Email2DisplayName,
			ContactSchema.Email3DisplayName
		}), delegate(RetrieverProducer producer, object value)
		{
			((IUpdateableListField<string>)producer.Holder[producer.EmailDisplayNameIndex]).List = (List<string>)value;
		});

		// Token: 0x04000025 RID: 37
		[FastDocumentSchema.FastPropertyDefinitionAttribute(FastDocumentSchema.PropertyDefinitionItemTypes.Contact)]
		public static readonly FastPropertyDefinition EmailAddress = new FastPropertyDefinition(FastIndexSystemSchema.EmailAddress, PropertyDefinitionAttribute.None, new StorePropertyDefinition[]
		{
			ContactSchema.Email1EmailAddress,
			ContactSchema.Email2EmailAddress,
			ContactSchema.Email3EmailAddress
		}, delegate(Item item)
		{
			List<string> list = new List<string>();
			Contact contact = item as Contact;
			if (contact == null)
			{
				DistributionList distributionList = item as DistributionList;
				if (distributionList != null)
				{
					string text = SearchParticipant.ToParticipantString(distributionList.GetAsParticipant());
					if (!string.IsNullOrEmpty(text))
					{
						list.Add(text);
					}
				}
			}
			else
			{
				foreach (KeyValuePair<EmailAddressIndex, Participant> keyValuePair in contact.EmailAddresses)
				{
					string text2 = SearchParticipant.ToParticipantString(keyValuePair.Value);
					if (!string.IsNullOrEmpty(text2))
					{
						list.Add(text2);
					}
				}
			}
			return list;
		}, delegate(RetrieverProducer producer, object value)
		{
			((IUpdateableListField<string>)producer.Holder[producer.EmailAddressIndex]).List = (List<string>)value;
		});

		// Token: 0x04000026 RID: 38
		[FastDocumentSchema.FastPropertyDefinitionAttribute(FastDocumentSchema.PropertyDefinitionItemTypes.Contact)]
		public static readonly FastPropertyDefinition EmailOriginalDisplayName = new FastPropertyDefinition(FastIndexSystemSchema.EmailOriginalDisplayName, PropertyDefinitionAttribute.None, new StorePropertyDefinition[]
		{
			ContactSchema.Email1OriginalDisplayName,
			ContactSchema.Email2OriginalDisplayName,
			ContactSchema.Email3OriginalDisplayName
		}, (Item item) => FastDocumentSchema.GetPropertyValues(item, new StorePropertyDefinition[]
		{
			ContactSchema.Email1OriginalDisplayName,
			ContactSchema.Email2OriginalDisplayName,
			ContactSchema.Email3OriginalDisplayName
		}), delegate(RetrieverProducer producer, object value)
		{
			((IUpdateableListField<string>)producer.Holder[producer.EmailOriginalDisplayNameIndex]).List = (List<string>)value;
		});

		// Token: 0x04000027 RID: 39
		[FastDocumentSchema.FastPropertyDefinitionAttribute(FastDocumentSchema.PropertyDefinitionItemTypes.Contact)]
		public static readonly FastPropertyDefinition IMAddress = new FastPropertyDefinition(FastIndexSystemSchema.IMAddress, PropertyDefinitionAttribute.None, new StorePropertyDefinition[]
		{
			ContactSchema.IMAddress,
			ContactSchema.IMAddress2,
			ContactSchema.IMAddress3
		}, (Item item) => FastDocumentSchema.GetPropertyValues(item, new StorePropertyDefinition[]
		{
			ContactSchema.IMAddress,
			ContactSchema.IMAddress2,
			ContactSchema.IMAddress3
		}), delegate(RetrieverProducer producer, object value)
		{
			((IUpdateableListField<string>)producer.Holder[producer.IMAddressIndex]).List = (List<string>)value;
		});

		// Token: 0x04000028 RID: 40
		[FastDocumentSchema.FastPropertyDefinitionAttribute(FastDocumentSchema.PropertyDefinitionItemTypes.Contact)]
		public static readonly FastPropertyDefinition HomeAddress = new FastPropertyDefinition(FastIndexSystemSchema.HomeAddress, PropertyDefinitionAttribute.None, ContactSchema.HomeAddress, null, delegate(RetrieverProducer producer, object value)
		{
			((IUpdateableStringField)producer.Holder[producer.HomeAddressIndex]).StringValue = (((string)value) ?? string.Empty);
		});

		// Token: 0x04000029 RID: 41
		[FastDocumentSchema.FastPropertyDefinitionAttribute(FastDocumentSchema.PropertyDefinitionItemTypes.Contact)]
		public static readonly FastPropertyDefinition OtherAddress = new FastPropertyDefinition(FastIndexSystemSchema.OtherAddress, PropertyDefinitionAttribute.None, ContactSchema.OtherAddress, null, delegate(RetrieverProducer producer, object value)
		{
			((IUpdateableStringField)producer.Holder[producer.OtherAddressIndex]).StringValue = (((string)value) ?? string.Empty);
		});

		// Token: 0x0400002A RID: 42
		[FastDocumentSchema.FastPropertyDefinitionAttribute(FastDocumentSchema.PropertyDefinitionItemTypes.Contact)]
		public static readonly FastPropertyDefinition BusinessAddress = new FastPropertyDefinition(FastIndexSystemSchema.BusinessAddress, PropertyDefinitionAttribute.None, ContactSchema.BusinessAddress, null, delegate(RetrieverProducer producer, object value)
		{
			((IUpdateableStringField)producer.Holder[producer.BusinessAddressIndex]).StringValue = (((string)value) ?? string.Empty);
		});

		// Token: 0x0400002B RID: 43
		[FastDocumentSchema.FastPropertyDefinitionAttribute(FastDocumentSchema.PropertyDefinitionItemTypes.Contact)]
		public static readonly FastPropertyDefinition MiddleName = new FastPropertyDefinition(FastIndexSystemSchema.MiddleName, PropertyDefinitionAttribute.None, ContactSchema.MiddleName, null, delegate(RetrieverProducer producer, object value)
		{
			((IUpdateableStringField)producer.Holder[producer.MiddleNameIndex]).StringValue = (((string)value) ?? string.Empty);
		});

		// Token: 0x0400002C RID: 44
		[FastDocumentSchema.FastPropertyDefinitionAttribute(FastDocumentSchema.PropertyDefinitionItemTypes.Contact)]
		public static readonly FastPropertyDefinition Nickname = new FastPropertyDefinition(FastIndexSystemSchema.Nickname, PropertyDefinitionAttribute.None, ContactSchema.Nickname, null, delegate(RetrieverProducer producer, object value)
		{
			((IUpdateableStringField)producer.Holder[producer.NicknameIndex]).StringValue = (((string)value) ?? string.Empty);
		});

		// Token: 0x0400002D RID: 45
		[FastDocumentSchema.FastPropertyDefinitionAttribute(FastDocumentSchema.PropertyDefinitionItemTypes.Contact)]
		public static readonly FastPropertyDefinition YomiCompanyName = new FastPropertyDefinition(FastIndexSystemSchema.YomiCompanyName, PropertyDefinitionAttribute.None, ContactSchema.YomiCompany, null, delegate(RetrieverProducer producer, object value)
		{
			((IUpdateableStringField)producer.Holder[producer.YomiCompanyNameIndex]).StringValue = (((string)value) ?? string.Empty);
		});

		// Token: 0x0400002E RID: 46
		[FastDocumentSchema.FastPropertyDefinitionAttribute(FastDocumentSchema.PropertyDefinitionItemTypes.Contact)]
		public static readonly FastPropertyDefinition YomiFirstName = new FastPropertyDefinition(FastIndexSystemSchema.YomiFirstName, PropertyDefinitionAttribute.None, ContactSchema.YomiFirstName, null, delegate(RetrieverProducer producer, object value)
		{
			((IUpdateableStringField)producer.Holder[producer.YomiFirstNameIndex]).StringValue = (((string)value) ?? string.Empty);
		});

		// Token: 0x0400002F RID: 47
		[FastDocumentSchema.FastPropertyDefinitionAttribute(FastDocumentSchema.PropertyDefinitionItemTypes.Contact)]
		public static readonly FastPropertyDefinition YomiLastName = new FastPropertyDefinition(FastIndexSystemSchema.YomiLastName, PropertyDefinitionAttribute.None, ContactSchema.YomiLastName, null, delegate(RetrieverProducer producer, object value)
		{
			((IUpdateableStringField)producer.Holder[producer.YomiLastNameIndex]).StringValue = (((string)value) ?? string.Empty);
		});

		// Token: 0x04000030 RID: 48
		[FastDocumentSchema.FastPropertyDefinitionAttribute(FastDocumentSchema.PropertyDefinitionItemTypes.Contact)]
		public static readonly FastPropertyDefinition BusinessPhoneNumber = new FastPropertyDefinition(FastIndexSystemSchema.BusinessPhoneNumber, PropertyDefinitionAttribute.None, new StorePropertyDefinition[]
		{
			ContactSchema.BusinessPhoneNumber,
			ContactSchema.BusinessPhoneNumber2
		}, (Item item) => FastDocumentSchema.GetPropertyValues(item, new StorePropertyDefinition[]
		{
			ContactSchema.BusinessPhoneNumber,
			ContactSchema.BusinessPhoneNumber2
		}), delegate(RetrieverProducer producer, object value)
		{
			((IUpdateableListField<string>)producer.Holder[producer.BusinessPhoneNumberIndex]).List = (List<string>)value;
		});

		// Token: 0x04000031 RID: 49
		[FastDocumentSchema.FastPropertyDefinitionAttribute(FastDocumentSchema.PropertyDefinitionItemTypes.Contact)]
		public static readonly FastPropertyDefinition CarPhoneNumber = new FastPropertyDefinition(FastIndexSystemSchema.CarPhoneNumber, PropertyDefinitionAttribute.None, ContactSchema.CarPhone, null, delegate(RetrieverProducer producer, object value)
		{
			((IUpdateableStringField)producer.Holder[producer.CarPhoneNumberIndex]).StringValue = (((string)value) ?? string.Empty);
		});

		// Token: 0x04000032 RID: 50
		[FastDocumentSchema.FastPropertyDefinitionAttribute(FastDocumentSchema.PropertyDefinitionItemTypes.Contact)]
		public static readonly FastPropertyDefinition MobilePhoneNumber = new FastPropertyDefinition(FastIndexSystemSchema.MobilePhoneNumber, PropertyDefinitionAttribute.None, new StorePropertyDefinition[]
		{
			ContactSchema.MobilePhone,
			ContactSchema.MobilePhone2
		}, (Item item) => FastDocumentSchema.GetPropertyValues(item, new StorePropertyDefinition[]
		{
			ContactSchema.MobilePhone,
			ContactSchema.MobilePhone2
		}), delegate(RetrieverProducer producer, object value)
		{
			((IUpdateableListField<string>)producer.Holder[producer.MobilePhoneNumberIndex]).List = (List<string>)value;
		});

		// Token: 0x04000033 RID: 51
		[FastDocumentSchema.FastPropertyDefinitionAttribute(FastDocumentSchema.PropertyDefinitionItemTypes.Contact)]
		public static readonly FastPropertyDefinition BusinessMainPhone = new FastPropertyDefinition(FastIndexSystemSchema.BusinessMainPhone, PropertyDefinitionAttribute.None, ContactSchema.OrganizationMainPhone, null, delegate(RetrieverProducer producer, object value)
		{
			((IUpdateableStringField)producer.Holder[producer.BusinessMainPhoneIndex]).StringValue = (((string)value) ?? string.Empty);
		});

		// Token: 0x04000034 RID: 52
		[FastDocumentSchema.FastPropertyDefinitionAttribute(FastDocumentSchema.PropertyDefinitionItemTypes.Contact)]
		public static readonly FastPropertyDefinition CompanyName = new FastPropertyDefinition(FastIndexSystemSchema.CompanyName, PropertyDefinitionAttribute.None, ContactSchema.CompanyName, null, delegate(RetrieverProducer producer, object value)
		{
			((IUpdateableStringField)producer.Holder[producer.CompanyNameIndex]).StringValue = (((string)value) ?? string.Empty);
		});

		// Token: 0x04000035 RID: 53
		[FastDocumentSchema.FastPropertyDefinitionAttribute(FastDocumentSchema.PropertyDefinitionItemTypes.Contact)]
		public static readonly FastPropertyDefinition OfficeLocation = new FastPropertyDefinition(FastIndexSystemSchema.OfficeLocation, PropertyDefinitionAttribute.None, ContactSchema.OfficeLocation, null, delegate(RetrieverProducer producer, object value)
		{
			((IUpdateableStringField)producer.Holder[producer.OfficeLocationIndex]).StringValue = (((string)value) ?? string.Empty);
		});

		// Token: 0x04000036 RID: 54
		[FastDocumentSchema.FastPropertyDefinitionAttribute(FastDocumentSchema.PropertyDefinitionItemTypes.Contact)]
		public static readonly FastPropertyDefinition DepartmentName = new FastPropertyDefinition(FastIndexSystemSchema.DepartmentName, PropertyDefinitionAttribute.None, ContactSchema.Department, null, delegate(RetrieverProducer producer, object value)
		{
			((IUpdateableStringField)producer.Holder[producer.DepartmentNameIndex]).StringValue = (((string)value) ?? string.Empty);
		});

		// Token: 0x04000037 RID: 55
		[FastDocumentSchema.FastPropertyDefinitionAttribute(FastDocumentSchema.PropertyDefinitionItemTypes.Contact)]
		public static readonly FastPropertyDefinition DisplayNamePrefix = new FastPropertyDefinition(FastIndexSystemSchema.DisplayNamePrefix, PropertyDefinitionAttribute.None, ContactSchema.DisplayNamePrefix, null, delegate(RetrieverProducer producer, object value)
		{
			((IUpdateableStringField)producer.Holder[producer.DisplayNamePrefixIndex]).StringValue = (((string)value) ?? string.Empty);
		});

		// Token: 0x04000038 RID: 56
		[FastDocumentSchema.FastPropertyDefinitionAttribute(FastDocumentSchema.PropertyDefinitionItemTypes.Contact)]
		public static readonly FastPropertyDefinition HomePhone = new FastPropertyDefinition(FastIndexSystemSchema.HomePhone, PropertyDefinitionAttribute.None, new StorePropertyDefinition[]
		{
			ContactSchema.HomePhone,
			ContactSchema.HomePhone2
		}, (Item item) => FastDocumentSchema.GetPropertyValues(item, new StorePropertyDefinition[]
		{
			ContactSchema.HomePhone,
			ContactSchema.HomePhone2
		}), delegate(RetrieverProducer producer, object value)
		{
			((IUpdateableListField<string>)producer.Holder[producer.HomePhoneIndex]).List = (List<string>)value;
		});

		// Token: 0x04000039 RID: 57
		[FastDocumentSchema.FastPropertyDefinitionAttribute(FastDocumentSchema.PropertyDefinitionItemTypes.Contact)]
		public static readonly FastPropertyDefinition PrimaryTelephoneNumber = new FastPropertyDefinition(FastIndexSystemSchema.PrimaryTelephoneNumber, PropertyDefinitionAttribute.None, ContactSchema.PrimaryTelephoneNumber, null, delegate(RetrieverProducer producer, object value)
		{
			((IUpdateableStringField)producer.Holder[producer.PrimaryTelephoneNumberIndex]).StringValue = (((string)value) ?? string.Empty);
		});

		// Token: 0x0400003A RID: 58
		[FastDocumentSchema.FastPropertyDefinitionAttribute(FastDocumentSchema.PropertyDefinitionItemTypes.Contact)]
		public static readonly FastPropertyDefinition Title = new FastPropertyDefinition(FastIndexSystemSchema.Title, PropertyDefinitionAttribute.None, ContactSchema.Title, null, delegate(RetrieverProducer producer, object value)
		{
			((IUpdateableStringField)producer.Holder[producer.ContactTitleIndex]).StringValue = (((string)value) ?? string.Empty);
		});

		// Token: 0x0400003B RID: 59
		[FastDocumentSchema.FastPropertyDefinitionAttribute(FastDocumentSchema.PropertyDefinitionItemTypes.Task)]
		public static readonly FastPropertyDefinition TaskTitle = new FastPropertyDefinition(FastIndexSystemSchema.TaskTitle, PropertyDefinitionAttribute.None, ItemSchema.FlagSubject, null, delegate(RetrieverProducer producer, object value)
		{
			((IUpdateableStringField)producer.Holder[producer.TaskTitleIndex]).StringValue = (((string)value) ?? string.Empty);
		});

		// Token: 0x0400003C RID: 60
		[FastDocumentSchema.FastPropertyDefinitionAttribute(FastDocumentSchema.PropertyDefinitionItemTypes.VoiceMail)]
		public static readonly FastPropertyDefinition UmAudioNotes = new FastPropertyDefinition(FastIndexSystemSchema.UmAudioNotes, PropertyDefinitionAttribute.None, MessageItemSchema.MessageAudioNotes, null, delegate(RetrieverProducer producer, object value)
		{
			((IUpdateableStringField)producer.Holder[producer.UMAudioNotesIndex]).StringValue = (((string)value) ?? string.Empty);
		});

		// Token: 0x0400003D RID: 61
		[FastDocumentSchema.FastPropertyDefinitionAttribute(FastDocumentSchema.PropertyDefinitionItemTypes.Item)]
		public static readonly FastPropertyDefinition Importance = new FastPropertyDefinition(FastIndexSystemSchema.Importance, PropertyDefinitionAttribute.None, ItemSchema.Importance, null, delegate(RetrieverProducer producer, object value)
		{
			((IUpdateableInt32Field)producer.Holder[producer.ImportanceIndex]).Int32Value = (int)value;
		});

		// Token: 0x0400003E RID: 62
		[FastDocumentSchema.FastPropertyDefinitionAttribute(FastDocumentSchema.PropertyDefinitionItemTypes.Item)]
		public static readonly FastPropertyDefinition Size = new FastPropertyDefinition(FastIndexSystemSchema.Size, PropertyDefinitionAttribute.None, ItemSchema.Size, null, delegate(RetrieverProducer producer, object value)
		{
			((IUpdateableInt32Field)producer.Holder[producer.SizeIndex]).Int32Value = (int)value;
		});

		// Token: 0x0400003F RID: 63
		[FastDocumentSchema.FastPropertyDefinitionAttribute(FastDocumentSchema.PropertyDefinitionItemTypes.Item)]
		public static readonly FastPropertyDefinition ItemClass = new FastPropertyDefinition(FastIndexSystemSchema.ItemClass, PropertyDefinitionAttribute.None, StoreObjectSchema.ItemClass, null, delegate(RetrieverProducer producer, object value)
		{
			((IUpdateableStringField)producer.Holder[producer.ItemClassIndex]).StringValue = (((string)value) ?? string.Empty);
		});

		// Token: 0x04000040 RID: 64
		[FastDocumentSchema.FastPropertyDefinitionAttribute(FastDocumentSchema.PropertyDefinitionItemTypes.Item)]
		public static readonly FastPropertyDefinition Categories = new FastPropertyDefinition(FastIndexSystemSchema.Categories, PropertyDefinitionAttribute.None, ItemSchema.Categories, null, delegate(RetrieverProducer producer, object value)
		{
			if (value != null)
			{
				string[] array = (string[])value;
				foreach (string text in array)
				{
					((IUpdateableListField<string>)producer.Holder[producer.CategoriesIndex]).Add(text);
				}
			}
		});

		// Token: 0x04000041 RID: 65
		[FastDocumentSchema.FastPropertyDefinitionAttribute(FastDocumentSchema.PropertyDefinitionItemTypes.Item)]
		public static readonly FastPropertyDefinition ConversationId = new FastPropertyDefinition(FastIndexSystemSchema.ConversationId, PropertyDefinitionAttribute.None, ItemSchema.ConversationDocumentId, (Item item) => item.GetValueOrDefault<int>(ItemSchema.ConversationDocumentId, 0), delegate(RetrieverProducer producer, object value)
		{
			((IUpdateableInt32Field)producer.Holder[producer.ConversationIdIndex]).Int32Value = (int)value;
		});

		// Token: 0x04000042 RID: 66
		[FastDocumentSchema.FastPropertyDefinitionAttribute(FastDocumentSchema.PropertyDefinitionItemTypes.Item)]
		public static readonly FastPropertyDefinition IsRead = new FastPropertyDefinition(FastIndexSystemSchema.IsRead, PropertyDefinitionAttribute.None, MessageItemSchema.IsRead, (Item item) => item.GetValueOrDefault<bool>(MessageItemSchema.IsRead, false), delegate(RetrieverProducer producer, object value)
		{
			((IUpdateableBooleanField)producer.Holder[producer.IsReadIndex]).BooleanValue = (bool)value;
		});

		// Token: 0x04000043 RID: 67
		[FastDocumentSchema.FastPropertyDefinitionAttribute(FastDocumentSchema.PropertyDefinitionItemTypes.Item)]
		public static readonly FastPropertyDefinition IconIndex = new FastPropertyDefinition(FastIndexSystemSchema.IconIndex, PropertyDefinitionAttribute.None, ItemSchema.IconIndex, (Item item) => item.GetValueOrDefault<IconIndex>(ItemSchema.IconIndex, Microsoft.Exchange.Data.Storage.IconIndex.Default), delegate(RetrieverProducer producer, object value)
		{
			((IUpdateableInt32Field)producer.Holder[producer.IconIndexIndex]).Int32Value = (int)value;
		});

		// Token: 0x04000044 RID: 68
		[FastDocumentSchema.FastPropertyDefinitionAttribute(FastDocumentSchema.PropertyDefinitionItemTypes.Item)]
		public static readonly FastPropertyDefinition HasAttachment = new FastPropertyDefinition(FastIndexSystemSchema.HasAttachment, PropertyDefinitionAttribute.None, ItemSchema.HasAttachment, (Item item) => item.GetValueOrDefault<bool>(ItemSchema.HasAttachment, false), delegate(RetrieverProducer producer, object value)
		{
			((IUpdateableBooleanField)producer.Holder[producer.HasAttachmentIndex]).BooleanValue = (bool)value;
		});

		// Token: 0x04000045 RID: 69
		[FastDocumentSchema.FastPropertyDefinitionAttribute(FastDocumentSchema.PropertyDefinitionItemTypes.Item)]
		public static readonly FastPropertyDefinition Mid = new FastPropertyDefinition(FastIndexSystemSchema.Mid, PropertyDefinitionAttribute.None, MessageItemSchema.MID, null, delegate(RetrieverProducer producer, object value)
		{
			((IUpdateableInt64Field)producer.Holder[producer.MidIndex]).Int64Value = (long)value;
		});

		// Token: 0x04000046 RID: 70
		[FastDocumentSchema.FastPropertyDefinitionAttribute(FastDocumentSchema.PropertyDefinitionItemTypes.Item)]
		public static readonly FastPropertyDefinition FlagStatus = new FastPropertyDefinition(FastIndexSystemSchema.FlagStatus, PropertyDefinitionAttribute.None, ItemSchema.FlagStatus, (Item item) => (int)item.GetValueOrDefault<FlagStatus>(ItemSchema.FlagStatus, Microsoft.Exchange.Data.Storage.FlagStatus.NotFlagged), delegate(RetrieverProducer producer, object value)
		{
			((IUpdateableInt32Field)producer.Holder[producer.FlagStatusIndex]).Int32Value = (int)value;
		});

		// Token: 0x04000047 RID: 71
		[FastDocumentSchema.FastPropertyDefinitionAttribute(FastDocumentSchema.PropertyDefinitionItemTypes.Item)]
		public static readonly FastPropertyDefinition BodyPreview = new FastPropertyDefinition(FastIndexSystemSchema.BodyPreview, PropertyDefinitionAttribute.SkipValueTracing, ItemSchema.Preview, delegate(Item item)
		{
			string preview = item.Preview;
			return preview.Substring(0, Math.Min(preview.Length, 60));
		}, delegate(RetrieverProducer producer, object value)
		{
			((IUpdateableStringField)producer.Holder[producer.BodyPreviewIndex]).StringValue = (string)value;
		});

		// Token: 0x04000048 RID: 72
		[FastDocumentSchema.FastPropertyDefinitionAttribute(FastDocumentSchema.PropertyDefinitionItemTypes.Item)]
		public static readonly FastPropertyDefinition ConversationGuid = new FastPropertyDefinition(FastIndexSystemSchema.ConversationGuid, PropertyDefinitionAttribute.None, ItemSchema.ConversationId, null, delegate(RetrieverProducer producer, object value)
		{
			((IUpdateableBlobField)producer.Holder[producer.ConversationGuidIndex]).BlobValue = ((ConversationId)value).GetBytes();
		});

		// Token: 0x04000049 RID: 73
		[FastDocumentSchema.FastPropertyDefinitionAttribute(FastDocumentSchema.PropertyDefinitionItemTypes.Item)]
		public static readonly FastPropertyDefinition WorkingSetId = new FastPropertyDefinition(FastIndexSystemSchema.WorkingSetId, PropertyDefinitionAttribute.None, ItemSchema.WorkingSetId, null, delegate(RetrieverProducer producer, object value)
		{
			((IUpdateableStringField)producer.Holder[producer.WorkingSetIdIndex]).StringValue = (string)value;
		});

		// Token: 0x0400004A RID: 74
		[FastDocumentSchema.FastPropertyDefinitionAttribute(FastDocumentSchema.PropertyDefinitionItemTypes.Item)]
		public static readonly FastPropertyDefinition WorkingSetSource = new FastPropertyDefinition(FastIndexSystemSchema.WorkingSetSource, PropertyDefinitionAttribute.None, ItemSchema.WorkingSetSource, null, delegate(RetrieverProducer producer, object value)
		{
			((IUpdateableInt32Field)producer.Holder[producer.WorkingSetSourceIndex]).Int32Value = (int)value;
		});

		// Token: 0x0400004B RID: 75
		[FastDocumentSchema.FastPropertyDefinitionAttribute(FastDocumentSchema.PropertyDefinitionItemTypes.Item)]
		public static readonly FastPropertyDefinition WorkingSetSourcePartition = new FastPropertyDefinition(FastIndexSystemSchema.WorkingSetSourcePartition, PropertyDefinitionAttribute.None, ItemSchema.WorkingSetSourcePartition, null, delegate(RetrieverProducer producer, object value)
		{
			((IUpdateableStringField)producer.Holder[producer.WorkingSetSourcePartitionIndex]).StringValue = (string)value;
		});

		// Token: 0x0400004C RID: 76
		[FastDocumentSchema.FastPropertyDefinitionAttribute(FastDocumentSchema.PropertyDefinitionItemTypes.Item)]
		public static readonly FastPropertyDefinition WorkingSetFlags = new FastPropertyDefinition(FastIndexSystemSchema.WorkingSetFlags, PropertyDefinitionAttribute.None, ItemSchema.WorkingSetFlags, (Item item) => item.GetValueOrDefault<WorkingSetFlags>(ItemSchema.WorkingSetFlags, Microsoft.Exchange.Data.Storage.WorkingSetFlags.Exchange), delegate(RetrieverProducer producer, object value)
		{
			WorkingSetFlags workingSetStoreFlags = (WorkingSetFlags)value;
			((IUpdateableListField<string>)producer.Holder[producer.WorkingSetFlagsIndex]).List = FastDocumentSchema.GetWorkingSetIndexFlags(workingSetStoreFlags);
		});

		// Token: 0x0400004D RID: 77
		private static readonly object lockObject = new object();

		// Token: 0x0400004E RID: 78
		private static List<StringBuilder> stringBuilderPoolList = new List<StringBuilder>(100);

		// Token: 0x0400004F RID: 79
		private static List<char[]> charBuffers = new List<char[]>(100);

		// Token: 0x04000050 RID: 80
		private static Array workingSetFlags;

		// Token: 0x04000051 RID: 81
		private static FastDocumentSchema instance;

		// Token: 0x0200000D RID: 13
		[Flags]
		internal enum PropertyDefinitionItemTypes
		{
			// Token: 0x040000C1 RID: 193
			Item = 1,
			// Token: 0x040000C2 RID: 194
			Message = 2,
			// Token: 0x040000C3 RID: 195
			Contact = 4,
			// Token: 0x040000C4 RID: 196
			Attachment = 8,
			// Token: 0x040000C5 RID: 197
			Meeting = 16,
			// Token: 0x040000C6 RID: 198
			SharingRequest = 32,
			// Token: 0x040000C7 RID: 199
			Task = 64,
			// Token: 0x040000C8 RID: 200
			VoiceMail = 128,
			// Token: 0x040000C9 RID: 201
			Calendar = 256
		}

		// Token: 0x0200000E RID: 14
		[AttributeUsage(AttributeTargets.Field)]
		private sealed class FastPropertyDefinitionAttribute : Attribute
		{
			// Token: 0x060000BC RID: 188 RVA: 0x00005CAD File Offset: 0x00003EAD
			public FastPropertyDefinitionAttribute(FastDocumentSchema.PropertyDefinitionItemTypes types)
			{
				this.Types = types;
			}

			// Token: 0x17000018 RID: 24
			// (get) Token: 0x060000BD RID: 189 RVA: 0x00005CBC File Offset: 0x00003EBC
			// (set) Token: 0x060000BE RID: 190 RVA: 0x00005CC4 File Offset: 0x00003EC4
			public FastDocumentSchema.PropertyDefinitionItemTypes Types { get; private set; }
		}
	}
}
