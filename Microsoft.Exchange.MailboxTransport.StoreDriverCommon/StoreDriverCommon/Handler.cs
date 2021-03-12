using System;
using System.Reflection;
using Microsoft.Exchange.Data.Transport.Smtp;

namespace Microsoft.Exchange.MailboxTransport.StoreDriverCommon
{
	// Token: 0x02000018 RID: 24
	internal class Handler
	{
		// Token: 0x06000079 RID: 121 RVA: 0x00004522 File Offset: 0x00002722
		public Handler()
		{
			this.RetryInterval = RetryInterval.None;
			this.Action = MessageAction.Retry;
			this.Category = Category.Transient;
			this.IncludeDiagnosticStatusText = true;
			this.ProcessInnerException = false;
			this.AppliesToAllRecipients = false;
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600007A RID: 122 RVA: 0x00004554 File Offset: 0x00002754
		// (set) Token: 0x0600007B RID: 123 RVA: 0x0000455C File Offset: 0x0000275C
		public MessageAction Action { get; set; }

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600007C RID: 124 RVA: 0x00004565 File Offset: 0x00002765
		// (set) Token: 0x0600007D RID: 125 RVA: 0x0000456D File Offset: 0x0000276D
		public TimeSpan? SpecifiedRetryInterval { get; set; }

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x0600007E RID: 126 RVA: 0x00004576 File Offset: 0x00002776
		// (set) Token: 0x0600007F RID: 127 RVA: 0x0000457E File Offset: 0x0000277E
		public SmtpResponse Response { get; set; }

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000080 RID: 128 RVA: 0x00004587 File Offset: 0x00002787
		// (set) Token: 0x06000081 RID: 129 RVA: 0x0000458F File Offset: 0x0000278F
		public bool AppliesToAllRecipients { get; set; }

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000082 RID: 130 RVA: 0x00004598 File Offset: 0x00002798
		// (set) Token: 0x06000083 RID: 131 RVA: 0x000045A0 File Offset: 0x000027A0
		public Category Category { get; set; }

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000084 RID: 132 RVA: 0x000045A9 File Offset: 0x000027A9
		// (set) Token: 0x06000085 RID: 133 RVA: 0x000045B1 File Offset: 0x000027B1
		public RetryInterval RetryInterval { get; set; }

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000086 RID: 134 RVA: 0x000045BA File Offset: 0x000027BA
		// (set) Token: 0x06000087 RID: 135 RVA: 0x000045C2 File Offset: 0x000027C2
		public Func<Exception, string> PrimaryStatusTextCallback { get; set; }

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000088 RID: 136 RVA: 0x000045CB File Offset: 0x000027CB
		// (set) Token: 0x06000089 RID: 137 RVA: 0x000045D3 File Offset: 0x000027D3
		public string PrimaryStatusText { get; set; }

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x0600008A RID: 138 RVA: 0x000045DC File Offset: 0x000027DC
		// (set) Token: 0x0600008B RID: 139 RVA: 0x000045E4 File Offset: 0x000027E4
		public Func<Exception, string> EnhancedStatusCodeCallback { get; set; }

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x0600008C RID: 140 RVA: 0x000045ED File Offset: 0x000027ED
		// (set) Token: 0x0600008D RID: 141 RVA: 0x000045F5 File Offset: 0x000027F5
		public string EnhancedStatusCode { get; set; }

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x0600008E RID: 142 RVA: 0x000045FE File Offset: 0x000027FE
		// (set) Token: 0x0600008F RID: 143 RVA: 0x00004606 File Offset: 0x00002806
		public bool IncludeDiagnosticStatusText { get; set; }

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000090 RID: 144 RVA: 0x0000460F File Offset: 0x0000280F
		// (set) Token: 0x06000091 RID: 145 RVA: 0x00004617 File Offset: 0x00002817
		public bool ProcessInnerException { get; set; }

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000092 RID: 146 RVA: 0x00004620 File Offset: 0x00002820
		// (set) Token: 0x06000093 RID: 147 RVA: 0x00004628 File Offset: 0x00002828
		public Action<Exception, IMessageConverter, MessageStatus> CustomizeStatusCallback { get; set; }

		// Token: 0x06000094 RID: 148 RVA: 0x00004634 File Offset: 0x00002834
		public static Handler Parse(string serializedProperties)
		{
			Handler handler = new Handler();
			Type type = handler.GetType();
			string[] array = serializedProperties.Split(new char[]
			{
				';'
			});
			foreach (string text in array)
			{
				string[] array3 = text.Trim().Split(new char[]
				{
					'='
				});
				if (array3.Length != 2)
				{
					throw new HandlerParseException(string.Format("Unable to parse Handler properties. Error: {0}", array3));
				}
				string text2 = array3[0];
				PropertyInfo property = Handler.GetProperty(type, text2);
				if (property == null)
				{
					throw new HandlerParseException(string.Format("Unable to parse Handler properties. Error when retrieving property: {0}", text2));
				}
				if (!Handler.IsSerializationSupported(property.PropertyType))
				{
					throw new HandlerParseException(string.Format("Unable to parse Handler properties. Serialization of {0} properties not supported.", property.PropertyType));
				}
				object obj = array3[1];
				if (property.PropertyType.IsEnum)
				{
					obj = Handler.ParseEnumValue(array3[1], text2, property);
				}
				else
				{
					obj = Handler.ConvertValueToPropertyType(text2, property, obj);
				}
				Handler.SetPropertyValue(handler, text2, property, obj);
			}
			return handler;
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00004750 File Offset: 0x00002950
		public MessageStatus CreateStatus(Exception exception, Exception exceptionAssociatedWithHandler, IMessageConverter converter, TimeSpan fastRetryInterval, TimeSpan quarantineRetryInterval)
		{
			SmtpResponse exceptionSmtpResponse = StorageExceptionHandler.GetExceptionSmtpResponse(converter, exception, this.Category == Category.Permanent, this.GetEffectiveStatusCode(), this.GetEffectiveEnhancedStatusCode(exceptionAssociatedWithHandler), this.GetEffectivePrimaryStatusText(exceptionAssociatedWithHandler), this.IncludeDiagnosticStatusText);
			MessageStatus messageStatus = new MessageStatus(this.Action, exceptionSmtpResponse, exception, this.AppliesToAllRecipients);
			if (this.SpecifiedRetryInterval != null)
			{
				messageStatus.RetryInterval = new TimeSpan?(this.SpecifiedRetryInterval.Value);
			}
			else if (this.RetryInterval != RetryInterval.None)
			{
				if (this.RetryInterval == RetryInterval.QuarantinedRetry)
				{
					messageStatus.RetryInterval = new TimeSpan?(quarantineRetryInterval);
				}
				else
				{
					messageStatus.RetryInterval = new TimeSpan?(fastRetryInterval);
				}
			}
			if (this.CustomizeStatusCallback != null)
			{
				this.CustomizeStatusCallback(exceptionAssociatedWithHandler, converter, messageStatus);
			}
			return messageStatus;
		}

		// Token: 0x06000096 RID: 150 RVA: 0x0000480C File Offset: 0x00002A0C
		private static void SetPropertyValue(Handler handler, string propName, PropertyInfo pi, object value)
		{
			try
			{
				pi.SetValue(handler, value, null);
			}
			catch (ArgumentException innerException)
			{
				throw new HandlerParseException(string.Format("Unable to set Handler property {0}:{1}.", propName, value), innerException);
			}
			catch (TargetException innerException2)
			{
				throw new HandlerParseException(string.Format("Unable to set Handler property {0}:{1}.", propName, value), innerException2);
			}
			catch (TargetParameterCountException innerException3)
			{
				throw new HandlerParseException(string.Format("Unable to set Handler property {0}:{1}.", propName, value), innerException3);
			}
			catch (MethodAccessException innerException4)
			{
				throw new HandlerParseException(string.Format("Unable to set Handler property {0}:{1}.", propName, value), innerException4);
			}
			catch (TargetInvocationException innerException5)
			{
				throw new HandlerParseException(string.Format("Unable to set Handler property {0}:{1}.", propName, value), innerException5);
			}
		}

		// Token: 0x06000097 RID: 151 RVA: 0x000048CC File Offset: 0x00002ACC
		private static object ConvertValueToPropertyType(string propName, PropertyInfo pi, object serializedValue)
		{
			object obj = null;
			try
			{
				obj = Convert.ChangeType(serializedValue, pi.PropertyType);
			}
			catch (ArgumentNullException innerException)
			{
				throw new HandlerParseException(string.Format("Unable to parse Handler property {0}:{1}.", propName, obj), innerException);
			}
			catch (InvalidCastException innerException2)
			{
				throw new HandlerParseException(string.Format("Unable to parse Handler property {0}:{1}.", propName, obj), innerException2);
			}
			catch (FormatException innerException3)
			{
				throw new HandlerParseException(string.Format("Unable to parse Handler property {0}:{1}.", propName, obj), innerException3);
			}
			catch (OverflowException innerException4)
			{
				throw new HandlerParseException(string.Format("Unable to parse Handler property {0}:{1}.", propName, obj), innerException4);
			}
			return obj;
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00004974 File Offset: 0x00002B74
		private static object ParseEnumValue(string serializedValue, string propName, PropertyInfo pi)
		{
			object obj = null;
			try
			{
				obj = Enum.Parse(pi.PropertyType, serializedValue);
			}
			catch (ArgumentNullException innerException)
			{
				throw new HandlerParseException(string.Format("Unable to parse Handler enum property {0}:{1}.", propName, obj), innerException);
			}
			catch (ArgumentException innerException2)
			{
				throw new HandlerParseException(string.Format("Unable to parse Handler enum property {0}:{1}.", propName, obj), innerException2);
			}
			catch (OverflowException innerException3)
			{
				throw new HandlerParseException(string.Format("Unable to parse Handler enum property {0}:{1}.", propName, obj), innerException3);
			}
			return obj;
		}

		// Token: 0x06000099 RID: 153 RVA: 0x000049F8 File Offset: 0x00002BF8
		private static PropertyInfo GetProperty(Type handlerType, string propName)
		{
			PropertyInfo property;
			try
			{
				property = handlerType.GetProperty(propName);
			}
			catch (ArgumentNullException innerException)
			{
				throw new HandlerParseException(string.Format("Unable to parse Handler properties. Error when retrieving property: {0}", propName), innerException);
			}
			catch (AmbiguousMatchException innerException2)
			{
				throw new HandlerParseException(string.Format("Unable to parse Handler properties. Error when retrieving property: {0}", propName), innerException2);
			}
			return property;
		}

		// Token: 0x0600009A RID: 154 RVA: 0x00004A54 File Offset: 0x00002C54
		private static bool IsSerializationSupported(Type serializedType)
		{
			return serializedType != null && (serializedType.IsEnum || serializedType == typeof(string) || serializedType == typeof(bool));
		}

		// Token: 0x0600009B RID: 155 RVA: 0x00004A8E File Offset: 0x00002C8E
		private string GetEnhancedStatusCode(Exception e)
		{
			if (this.EnhancedStatusCodeCallback != null)
			{
				return this.EnhancedStatusCodeCallback(e);
			}
			return this.EnhancedStatusCode;
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00004AAB File Offset: 0x00002CAB
		private string GetPrimaryStatusText(Exception e)
		{
			if (this.PrimaryStatusTextCallback != null)
			{
				return this.PrimaryStatusTextCallback(e);
			}
			return this.PrimaryStatusText;
		}

		// Token: 0x0600009D RID: 157 RVA: 0x00004AC8 File Offset: 0x00002CC8
		private string GetEffectiveStatusCode()
		{
			if (!this.Response.Equals(SmtpResponse.Empty))
			{
				return this.Response.StatusCode;
			}
			return null;
		}

		// Token: 0x0600009E RID: 158 RVA: 0x00004AFC File Offset: 0x00002CFC
		private string GetEffectiveEnhancedStatusCode(Exception exception)
		{
			string enhancedStatusCode = this.GetEnhancedStatusCode(exception);
			if (!this.Response.Equals(SmtpResponse.Empty))
			{
				return this.Response.EnhancedStatusCode;
			}
			if (!string.IsNullOrEmpty(enhancedStatusCode))
			{
				return enhancedStatusCode;
			}
			return null;
		}

		// Token: 0x0600009F RID: 159 RVA: 0x00004B40 File Offset: 0x00002D40
		private string GetEffectivePrimaryStatusText(Exception exception)
		{
			string primaryStatusText = this.GetPrimaryStatusText(exception);
			if (!this.Response.Equals(SmtpResponse.Empty))
			{
				return this.Response.StatusText[0];
			}
			if (!string.IsNullOrEmpty(primaryStatusText))
			{
				return primaryStatusText;
			}
			return null;
		}
	}
}
