using System;
using System.IO;
using System.Text;
using Microsoft.Exchange.Data.Globalization;
using Microsoft.Exchange.Data.TextConverters;
using Microsoft.Exchange.Data.Transport.Email;
using Microsoft.Exchange.TextMatching;

namespace Microsoft.Exchange.MessagingPolicies.Rules
{
	// Token: 0x02000022 RID: 34
	internal sealed class MessageBodies : IContent
	{
		// Token: 0x06000135 RID: 309 RVA: 0x000074D9 File Offset: 0x000056D9
		public MessageBodies(EmailMessage message, int level)
		{
			this.message = message;
			this.level = level;
			this.Reset();
		}

		// Token: 0x06000136 RID: 310 RVA: 0x000074FC File Offset: 0x000056FC
		public void Reset()
		{
			this.firstBody = true;
		}

		// Token: 0x06000137 RID: 311 RVA: 0x00007508 File Offset: 0x00005708
		private static bool CtsMatcher(MultiMatcher matcher, RulesEvaluationContext context, MessageBodies.MessageBody body, int bodyLevel, int bodyPartNumber)
		{
			return matcher.IsMatch(body.Reader, string.Concat(new object[]
			{
				"tagMessageBody",
				bodyLevel,
				'.',
				bodyPartNumber
			}), 0, context);
		}

		// Token: 0x06000138 RID: 312 RVA: 0x00007555 File Offset: 0x00005755
		private MessageBodies.MessageBody GetNextBody()
		{
			if (this.firstBody)
			{
				this.firstBody = false;
				return MessageBodies.MessageBody.Create(this.message.Body);
			}
			return null;
		}

		// Token: 0x06000139 RID: 313 RVA: 0x00007598 File Offset: 0x00005798
		bool IContent.Matches(MultiMatcher matcher, RulesEvaluationContext context)
		{
			TransportRulesEvaluationContext transportRulesEvaluationContext = (TransportRulesEvaluationContext)context;
			return this.Matches((MessageBodies.MessageBody body, int bodyLevel, int bodyPartNumber) => MessageBodies.CtsMatcher(matcher, context, body, bodyLevel, bodyPartNumber));
		}

		// Token: 0x0600013A RID: 314 RVA: 0x000075D8 File Offset: 0x000057D8
		private bool Matches(MessageBodies.BodyMatchingDelegate matcher)
		{
			this.Reset();
			int num = 0;
			bool result;
			for (;;)
			{
				MessageBodies.MessageBody nextBody;
				MessageBodies.MessageBody messageBody = nextBody = this.GetNextBody();
				try
				{
					if (messageBody == null)
					{
						result = false;
						break;
					}
					if (matcher(messageBody, this.level, num))
					{
						result = true;
						break;
					}
				}
				finally
				{
					if (nextBody != null)
					{
						((IDisposable)nextBody).Dispose();
					}
				}
				num++;
			}
			return result;
		}

		// Token: 0x04000104 RID: 260
		private const string TagMessageBody = "tagMessageBody";

		// Token: 0x04000105 RID: 261
		private readonly int level;

		// Token: 0x04000106 RID: 262
		private readonly EmailMessage message;

		// Token: 0x04000107 RID: 263
		private bool firstBody = true;

		// Token: 0x02000023 RID: 35
		// (Invoke) Token: 0x0600013C RID: 316
		private delegate bool BodyMatchingDelegate(MessageBodies.MessageBody body, int level, int bodyPartNumber);

		// Token: 0x02000024 RID: 36
		internal class MessageBody : ITextInputBuffer, IDisposable
		{
			// Token: 0x17000051 RID: 81
			// (get) Token: 0x0600013F RID: 319 RVA: 0x00007638 File Offset: 0x00005838
			public TextReader Reader
			{
				get
				{
					return this.reader;
				}
			}

			// Token: 0x06000140 RID: 320 RVA: 0x00007640 File Offset: 0x00005840
			private MessageBody(BodyFormat bodyFormat, Stream stream, int codePage)
			{
				TextConverter converter;
				switch (bodyFormat)
				{
				case BodyFormat.Text:
				{
					TextToText textToText = new TextToText
					{
						InputEncoding = Charset.GetEncoding(codePage)
					};
					converter = textToText;
					break;
				}
				case BodyFormat.Rtf:
				{
					RtfToText rtfToText = new RtfToText();
					converter = rtfToText;
					break;
				}
				case BodyFormat.Html:
				{
					HtmlToText htmlToText = new HtmlToText
					{
						InputEncoding = Charset.GetEncoding(codePage)
					};
					converter = htmlToText;
					break;
				}
				default:
					throw new ArgumentOutOfRangeException(string.Format("Parameter bodyFormat is out of range: '{0}'", bodyFormat));
				}
				this.reader = new ConverterReader(stream, converter);
			}

			// Token: 0x06000141 RID: 321 RVA: 0x000076D7 File Offset: 0x000058D7
			internal static MessageBodies.MessageBody Create(BodyFormat bodyFormat, Stream stream, int codePage)
			{
				return new MessageBodies.MessageBody(bodyFormat, stream, codePage);
			}

			// Token: 0x06000142 RID: 322 RVA: 0x000076E4 File Offset: 0x000058E4
			internal static MessageBodies.MessageBody Create(Body body)
			{
				if (body == null)
				{
					return null;
				}
				BodyFormat bodyFormat = body.BodyFormat;
				if (bodyFormat != BodyFormat.Text && bodyFormat != BodyFormat.Html && bodyFormat != BodyFormat.Rtf)
				{
					return null;
				}
				Stream rawContentReadStream;
				if (!body.TryGetContentReadStream(out rawContentReadStream))
				{
					if (body.MimePart == null)
					{
						return null;
					}
					rawContentReadStream = body.MimePart.GetRawContentReadStream();
				}
				string charsetName = body.CharsetName;
				Encoding ascii;
				if (charsetName == null || !Charset.TryGetEncoding(charsetName, out ascii))
				{
					ascii = Encoding.ASCII;
				}
				return new MessageBodies.MessageBody(bodyFormat, rawContentReadStream, ascii.CodePage);
			}

			// Token: 0x06000143 RID: 323 RVA: 0x00007752 File Offset: 0x00005952
			public void Dispose()
			{
				this.reader.Close();
			}

			// Token: 0x17000052 RID: 82
			// (get) Token: 0x06000144 RID: 324 RVA: 0x00007760 File Offset: 0x00005960
			public int NextChar
			{
				get
				{
					if (this.firstRead)
					{
						this.firstRead = false;
						return 1;
					}
					int num = this.reader.Read();
					if (-1 == num)
					{
						return -1;
					}
					return (int)char.ToLowerInvariant((char)num);
				}
			}

			// Token: 0x04000108 RID: 264
			private readonly TextReader reader;

			// Token: 0x04000109 RID: 265
			private bool firstRead = true;
		}
	}
}
