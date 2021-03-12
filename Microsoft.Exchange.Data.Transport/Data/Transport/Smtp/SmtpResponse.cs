using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;

namespace Microsoft.Exchange.Data.Transport.Smtp
{
	// Token: 0x02000024 RID: 36
	public struct SmtpResponse : IEquatable<SmtpResponse>
	{
		// Token: 0x060000AD RID: 173 RVA: 0x00002E6C File Offset: 0x0000106C
		public SmtpResponse(string statusCode, string enhancedStatusCode, params string[] statusText)
		{
			this = default(SmtpResponse);
			if (!SmtpResponseParser.IsValidStatusCode(statusCode))
			{
				throw new FormatException(string.Format("The SMTP status code must have three digits - statusCode: {0} statusText: {1}", statusCode, (statusText == null) ? "null" : string.Join(";", statusText)));
			}
			if (!string.IsNullOrEmpty(enhancedStatusCode) && !EnhancedStatusCodeImpl.IsValid(enhancedStatusCode))
			{
				throw new FormatException(string.Format("The SMTP enhanced status code must be in the form 2.yyy.zzz, 4.yyy.zzz, 5.yyy.zzz - {0}statusText: {1}", enhancedStatusCode, (statusText == null) ? "null" : string.Join(";", statusText)));
			}
			this.DsnExplanation = string.Empty;
			this.bytes = null;
			List<string> list = new List<string>();
			if (statusText != null)
			{
				foreach (string text in statusText)
				{
					if (!string.IsNullOrEmpty(text))
					{
						string[] array = SmtpResponse.SplitLines(text);
						for (int j = 0; j < array.Length; j++)
						{
							list.Add(array[j]);
						}
					}
				}
				foreach (string text2 in list)
				{
					for (int k = 0; k < text2.Length; k++)
					{
						if (text2[k] < '\u0001' || text2[k] > '\u007f')
						{
							throw new FormatException(string.Format("Text for the response must contain only US-ASCII characters - {0}", text2));
						}
					}
				}
			}
			this.data = new string[2 + list.Count];
			this.data[0] = statusCode;
			this.data[1] = enhancedStatusCode;
			list.CopyTo(this.data, 2);
			this.statusText = null;
		}

		// Token: 0x060000AE RID: 174 RVA: 0x00002FFC File Offset: 0x000011FC
		public SmtpResponse(string statusCode, string enhancedStatusCode, string dsnExplanation, bool overloadDifferentiator, params string[] statusText)
		{
			this = new SmtpResponse(statusCode, enhancedStatusCode, statusText);
			this.DsnExplanation = dsnExplanation;
		}

		// Token: 0x060000AF RID: 175 RVA: 0x0000300F File Offset: 0x0000120F
		private SmtpResponse(bool isEmpty)
		{
			this = default(SmtpResponse);
			this.empty = isEmpty;
			this.data = null;
			this.bytes = null;
			this.statusText = null;
			this.DsnExplanation = string.Empty;
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x060000B0 RID: 176 RVA: 0x0000303F File Offset: 0x0000123F
		public bool IsEmpty
		{
			get
			{
				return this.empty;
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x060000B1 RID: 177 RVA: 0x00003047 File Offset: 0x00001247
		// (set) Token: 0x060000B2 RID: 178 RVA: 0x0000304F File Offset: 0x0000124F
		public string DsnExplanation { get; private set; }

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x060000B3 RID: 179 RVA: 0x00003058 File Offset: 0x00001258
		public SmtpResponseType SmtpResponseType
		{
			get
			{
				if (string.IsNullOrEmpty(this.StatusCode))
				{
					return SmtpResponseType.Unknown;
				}
				switch (this.StatusCode[0])
				{
				case '2':
					return SmtpResponseType.Success;
				case '3':
					return SmtpResponseType.IntermediateSuccess;
				case '4':
					return SmtpResponseType.TransientError;
				case '5':
					return SmtpResponseType.PermanentError;
				default:
					return SmtpResponseType.Unknown;
				}
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x060000B4 RID: 180 RVA: 0x000030A5 File Offset: 0x000012A5
		public string StatusCode
		{
			get
			{
				if (this.data != null && this.data.Length >= 2)
				{
					return this.data[0];
				}
				return string.Empty;
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x060000B5 RID: 181 RVA: 0x000030C8 File Offset: 0x000012C8
		public string EnhancedStatusCode
		{
			get
			{
				if (this.data != null && this.data.Length >= 2)
				{
					return this.data[1];
				}
				return string.Empty;
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x060000B6 RID: 182 RVA: 0x000030EC File Offset: 0x000012EC
		public string[] StatusText
		{
			get
			{
				if (this.statusText == null)
				{
					if (this.data == null || this.data.Length < 2)
					{
						return null;
					}
					string[] array = new string[this.data.Length - 2];
					int num = 0;
					for (int i = 2; i < this.data.Length; i++)
					{
						array[num++] = this.data[i];
					}
					this.statusText = array;
				}
				return this.statusText;
			}
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x00003158 File Offset: 0x00001358
		public static SmtpResponse QueuedMailForDelivery(string messageId)
		{
			return new SmtpResponse("250", "2.6.0", new string[]
			{
				messageId + " Queued mail for delivery"
			});
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x0000318C File Offset: 0x0000138C
		public static SmtpResponse ConnectionDroppedDueTo(string reason)
		{
			if (string.IsNullOrEmpty(reason))
			{
				return SmtpResponse.ConnectionDropped;
			}
			return new SmtpResponse("421", "4.4.2", new string[]
			{
				"Connection dropped due to " + reason
			});
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x000031CC File Offset: 0x000013CC
		public static SmtpResponse QueuedMailForDelivery(string messageId, string recordId, string hostname, string smtpCustomDataResponse)
		{
			return new SmtpResponse("250", "2.6.0", new string[]
			{
				string.Format("{0} [InternalId={1}, Hostname={2}] {3}", new object[]
				{
					messageId,
					recordId,
					hostname,
					smtpCustomDataResponse
				})
			});
		}

		// Token: 0x060000BA RID: 186 RVA: 0x00003218 File Offset: 0x00001418
		public static SmtpResponse QueuedMailForRedundancy(string messageId)
		{
			if (string.IsNullOrEmpty(messageId))
			{
				throw new ArgumentNullException("messageId");
			}
			return new SmtpResponse("250", "2.6.0", new string[]
			{
				messageId + " Queued mail for redundancy"
			});
		}

		// Token: 0x060000BB RID: 187 RVA: 0x0000325D File Offset: 0x0000145D
		public static SmtpResponse QueuedMailForRedundancy(string messageId, string recordId, string hostname)
		{
			if (string.IsNullOrEmpty(messageId))
			{
				throw new ArgumentNullException("messageId");
			}
			if (string.IsNullOrEmpty(recordId))
			{
				throw new ArgumentNullException("recordId");
			}
			return SmtpResponse.QueuedMailForRedundancy(string.Format("{0} [InternalId={1}, Hostname={2}]", messageId, hostname, recordId));
		}

		// Token: 0x060000BC RID: 188 RVA: 0x00003298 File Offset: 0x00001498
		public static SmtpResponse ResourceForestMismatch(string expectedForest, string actualForest)
		{
			string text = string.Format("Temporary Server Network Configuration error detected: Expected={0}, actual={1}", expectedForest, actualForest);
			return new SmtpResponse("421", "4.3.2", new string[]
			{
				text
			});
		}

		// Token: 0x060000BD RID: 189 RVA: 0x000032D0 File Offset: 0x000014D0
		public static bool TryParse(string text, out SmtpResponse response)
		{
			if (string.IsNullOrEmpty(text) || text.Length < 3)
			{
				response = SmtpResponse.Empty;
				return false;
			}
			string[] array;
			if (!SmtpResponseParser.Split(text, out array))
			{
				response = SmtpResponse.Empty;
				return false;
			}
			response = new SmtpResponse(false)
			{
				data = array
			};
			return true;
		}

		// Token: 0x060000BE RID: 190 RVA: 0x0000332C File Offset: 0x0000152C
		internal static bool TryParse(List<string> lines, out SmtpResponse response)
		{
			string[] array;
			if (!SmtpResponseParser.Split(lines, out array))
			{
				response = SmtpResponse.Empty;
				return false;
			}
			response = new SmtpResponse(false)
			{
				data = array
			};
			return true;
		}

		// Token: 0x060000BF RID: 191 RVA: 0x00003368 File Offset: 0x00001568
		internal static SmtpResponse Parse(string text)
		{
			SmtpResponse result;
			if (!SmtpResponse.TryParse(text, out result))
			{
				throw new ArgumentException(string.Format("Text '{0}' could not be parsed into an SMTP response", text), "text");
			}
			return result;
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x00003396 File Offset: 0x00001596
		public static bool operator ==(SmtpResponse response1, SmtpResponse response2)
		{
			return string.Equals(response1.StatusCode, response2.StatusCode, StringComparison.Ordinal) && string.Equals(response1.EnhancedStatusCode, response2.EnhancedStatusCode, StringComparison.Ordinal);
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x000033C4 File Offset: 0x000015C4
		public static bool operator !=(SmtpResponse response1, SmtpResponse response2)
		{
			return !(response1 == response2);
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x000033D0 File Offset: 0x000015D0
		public bool Equals(SmtpResponse comparand)
		{
			return this == comparand;
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x000033E0 File Offset: 0x000015E0
		public bool Equals(SmtpResponse other, SmtpResponseCompareOptions compareOptions)
		{
			if (!this.Equals(other))
			{
				return false;
			}
			if (compareOptions != SmtpResponseCompareOptions.IncludeTextComponent)
			{
				return true;
			}
			if (this.StatusText != null && other.StatusText != null)
			{
				return this.statusText.SequenceEqual(other.statusText, StringComparer.OrdinalIgnoreCase);
			}
			return this.StatusText == null && other.StatusText == null;
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x0000343B File Offset: 0x0000163B
		public override bool Equals(object obj)
		{
			return obj is SmtpResponse && this == (SmtpResponse)obj;
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x00003458 File Offset: 0x00001658
		public override int GetHashCode()
		{
			int num = 0;
			if (!string.IsNullOrEmpty(this.StatusCode))
			{
				num = this.StatusCode.GetHashCode();
			}
			if (!string.IsNullOrEmpty(this.EnhancedStatusCode))
			{
				num ^= this.EnhancedStatusCode.GetHashCode();
			}
			return num;
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x0000349C File Offset: 0x0000169C
		public override string ToString()
		{
			byte[] array = this.ToByteArray();
			return CTSGlobals.AsciiEncoding.GetString(array, 0, array.Length - 2);
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x000034C4 File Offset: 0x000016C4
		internal static SmtpResponse AuthBlob(byte[] blob)
		{
			if (blob == null)
			{
				throw new ArgumentNullException("blob");
			}
			return new SmtpResponse("334", string.Empty, new string[]
			{
				Encoding.ASCII.GetString(blob)
			});
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x00003504 File Offset: 0x00001704
		internal static SmtpResponse ExchangeAuthSuccessful(byte[] blob)
		{
			if (blob == null)
			{
				throw new ArgumentNullException("blob");
			}
			return new SmtpResponse("235", string.Empty, new string[]
			{
				Encoding.ASCII.GetString(blob)
			});
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x00003544 File Offset: 0x00001744
		internal static SmtpResponse Banner(string serverName, string serverVersion, string date, bool isModernServer = false)
		{
			return new SmtpResponse("220", string.Empty, new string[]
			{
				string.Format("{0} {1} {2}", serverName, isModernServer ? "MICROSOFT ESMTP MAIL SERVICE READY AT" : "Microsoft ESMTP MAIL Service ready at", date)
			});
		}

		// Token: 0x060000CA RID: 202 RVA: 0x00003588 File Offset: 0x00001788
		internal static SmtpResponse Ehlo(string serverName, IPAddress ipAddress, long maxMessageSize, bool advertiseStartTls, bool advertiseAuth)
		{
			return new SmtpResponse("250", string.Empty, new string[]
			{
				string.Format("{0} Hello [{1}]", serverName, ipAddress),
				string.Format("SIZE {0}", maxMessageSize),
				"PIPELINING",
				"DSN",
				"ENHANCEDSTATUSCODES",
				advertiseStartTls ? "STARTTLS" : null,
				advertiseAuth ? "AUTH NTLM LOGIN" : null
			});
		}

		// Token: 0x060000CB RID: 203 RVA: 0x00003604 File Offset: 0x00001804
		internal static SmtpResponse Helo(string serverName, IPAddress ipAddress)
		{
			return new SmtpResponse("250", string.Empty, new string[]
			{
				string.Concat(new object[]
				{
					serverName,
					" Hello [",
					ipAddress,
					"]"
				})
			});
		}

		// Token: 0x060000CC RID: 204 RVA: 0x00003650 File Offset: 0x00001850
		internal static SmtpResponse OctetsReceived(long octets)
		{
			return new SmtpResponse("250", "2.6.0", new string[]
			{
				"CHUNK received OK, " + octets.ToString(CultureInfo.InvariantCulture) + " octets"
			});
		}

		// Token: 0x060000CD RID: 205 RVA: 0x00003694 File Offset: 0x00001894
		internal byte[] ToByteArray()
		{
			if (this.bytes == null)
			{
				string s;
				if (this.data != null && this.data.Length > 2)
				{
					string text;
					if (!string.IsNullOrEmpty(this.EnhancedStatusCode))
					{
						text = this.StatusCode + "-" + this.EnhancedStatusCode + " ";
					}
					else
					{
						text = this.StatusCode + "-";
					}
					int num = 0;
					for (int i = 2; i < this.data.Length; i++)
					{
						num += text.Length + this.data[i].Length + "\r\n".Length;
					}
					StringBuilder stringBuilder = new StringBuilder(num);
					int num2 = 0;
					for (int j = 2; j < this.data.Length; j++)
					{
						num2 = stringBuilder.Length;
						stringBuilder.Append(text);
						stringBuilder.Append(this.data[j]);
						stringBuilder.Append("\r\n");
					}
					if (stringBuilder.Length > 0)
					{
						num2 += this.StatusCode.Length;
						stringBuilder[num2] = ' ';
					}
					s = stringBuilder.ToString();
				}
				else if (!string.IsNullOrEmpty(this.EnhancedStatusCode))
				{
					s = this.StatusCode + " " + this.EnhancedStatusCode + "\r\n";
				}
				else if (!string.IsNullOrEmpty(this.StatusCode))
				{
					s = this.StatusCode + " " + "\r\n";
				}
				else
				{
					s = "\r\n";
				}
				this.bytes = CTSGlobals.AsciiEncoding.GetBytes(s);
			}
			return this.bytes;
		}

		// Token: 0x060000CE RID: 206 RVA: 0x00003828 File Offset: 0x00001A28
		private static string[] SplitLines(string response)
		{
			List<string> list = new List<string>();
			int num = 0;
			int num2;
			while ((num2 = response.IndexOf("\r\n", num, StringComparison.Ordinal)) != -1)
			{
				list.Add(response.Substring(num, num2 - num));
				num = num2 + "\r\n".Length;
				if (num >= response.Length)
				{
					break;
				}
			}
			if (num < response.Length)
			{
				list.Add(response.Substring(num));
			}
			return list.ToArray();
		}

		// Token: 0x04000050 RID: 80
		internal const int MaxResponseLineLength = 2000;

		// Token: 0x04000051 RID: 81
		internal const int MaxNumResponseLines = 50;

		// Token: 0x04000052 RID: 82
		internal const string ProxySessionProtocolFailurePrefixString = "Proxy session setup failed on Frontend with ";

		// Token: 0x04000053 RID: 83
		private const string CRLF = "\r\n";

		// Token: 0x04000054 RID: 84
		public static readonly SmtpResponse Empty = new SmtpResponse(true);

		// Token: 0x04000055 RID: 85
		public static readonly SmtpResponse BadCommandSequence = new SmtpResponse("503", "5.5.1", new string[]
		{
			"Bad sequence of commands"
		});

		// Token: 0x04000056 RID: 86
		public static readonly SmtpResponse ConnectionDroppedByAgentError = new SmtpResponse("421", "4.3.2", new string[]
		{
			"System not accepting network messages"
		});

		// Token: 0x04000057 RID: 87
		public static readonly SmtpResponse ConnectionTimedOut = new SmtpResponse("421", "4.4.1", new string[]
		{
			"Connection timed out"
		});

		// Token: 0x04000058 RID: 88
		public static readonly SmtpResponse DataTransactionFailed = new SmtpResponse("451", "4.3.2", new string[]
		{
			"System not accepting network messages"
		});

		// Token: 0x04000059 RID: 89
		public static readonly SmtpResponse RecipientAddressExpanded = new SmtpResponse("250", "2.0.0", new string[]
		{
			"Recipient address expanded"
		});

		// Token: 0x0400005A RID: 90
		public static readonly SmtpResponse RecipientAddressExpandedByRedirectionAgent = new SmtpResponse("250", "2.0.0", new string[]
		{
			"Recipient address expanded by Redirection Agent"
		});

		// Token: 0x0400005B RID: 91
		public static readonly SmtpResponse InsufficientResource = new SmtpResponse("452", "4.3.1", new string[]
		{
			"Insufficient system resources"
		});

		// Token: 0x0400005C RID: 92
		public static readonly SmtpResponse CTSParseError = new SmtpResponse("452", "4.6.0", new string[]
		{
			"Insufficient system resources (I/O)"
		});

		// Token: 0x0400005D RID: 93
		public static readonly SmtpResponse InvalidAddress = new SmtpResponse("501", "5.1.3", new string[]
		{
			"Invalid address"
		});

		// Token: 0x0400005E RID: 94
		public static readonly SmtpResponse InvalidSenderAddress = new SmtpResponse("501", "5.1.7", new string[]
		{
			"Invalid address"
		});

		// Token: 0x0400005F RID: 95
		public static readonly SmtpResponse SendAsDenied = new SmtpResponse("550", "5.7.1", new string[]
		{
			"Client does not have permissions to send as this sender"
		});

		// Token: 0x04000060 RID: 96
		public static readonly SmtpResponse AnonymousSendAsDenied = new SmtpResponse("550", "5.7.1", new string[]
		{
			"Anonymous client does not have permissions to send as this sender"
		});

		// Token: 0x04000061 RID: 97
		public static readonly SmtpResponse SendOnBehalfOfDenied = new SmtpResponse("550", "5.7.1", new string[]
		{
			"Client does not have permissions to send on behalf of the from address"
		});

		// Token: 0x04000062 RID: 98
		public static readonly SmtpResponse SubmitDenied = new SmtpResponse("550", "5.7.1", new string[]
		{
			"Client does not have permissions to submit to this server"
		});

		// Token: 0x04000063 RID: 99
		public static readonly SmtpResponse InvalidArguments = new SmtpResponse("501", "5.5.4", new string[]
		{
			"Invalid arguments"
		});

		// Token: 0x04000064 RID: 100
		public static readonly SmtpResponse TransientInvalidArguments = new SmtpResponse("401", "4.5.4", new string[]
		{
			"Invalid arguments - possible version mismatch"
		});

		// Token: 0x04000065 RID: 101
		public static readonly SmtpResponse InvalidContent = new SmtpResponse("554", "5.6.0", new string[]
		{
			"Invalid message content"
		});

		// Token: 0x04000066 RID: 102
		public static readonly SmtpResponse InvalidContentBareLinefeeds = new SmtpResponse("554", "5.6.0", new string[]
		{
			"Invalid message content, contains bare linefeeds"
		});

		// Token: 0x04000067 RID: 103
		public static readonly SmtpResponse MessagePartialNotSupported = new SmtpResponse("554", "5.6.1", new string[]
		{
			"Messages of type message/partial are not supported"
		});

		// Token: 0x04000068 RID: 104
		public static readonly SmtpResponse NoopOk = new SmtpResponse("250", "2.0.0", new string[]
		{
			"OK"
		});

		// Token: 0x04000069 RID: 105
		public static readonly SmtpResponse InvalidResponse = new SmtpResponse("421", "4.4.0", new string[]
		{
			"Remote server response was not RFC conformant"
		});

		// Token: 0x0400006A RID: 106
		public static readonly SmtpResponse MessageTooLarge = new SmtpResponse("552", "5.3.4", new string[]
		{
			"Message size exceeds fixed maximum message size"
		});

		// Token: 0x0400006B RID: 107
		public static readonly SmtpResponse ServiceUnavailable = new SmtpResponse("421", "4.3.2", new string[]
		{
			"Service not available"
		});

		// Token: 0x0400006C RID: 108
		public static readonly SmtpResponse RcptNotFound = new SmtpResponse("550", "5.1.1", new string[]
		{
			"User unknown"
		});

		// Token: 0x0400006D RID: 109
		public static readonly SmtpResponse SuccessfulConnection = new SmtpResponse("250", string.Empty, new string[]
		{
			"Success"
		});

		// Token: 0x0400006E RID: 110
		public static readonly SmtpResponse SystemMisconfiguration = new SmtpResponse("550", "5.3.5", new string[]
		{
			"System incorrectly configured"
		});

		// Token: 0x0400006F RID: 111
		public static readonly SmtpResponse UnableToRoute = new SmtpResponse("554", "5.4.4", new string[]
		{
			"Unable to route"
		});

		// Token: 0x04000070 RID: 112
		public static readonly SmtpResponse InvalidRecipientAddress = new SmtpResponse("554", "5.4.4", new string[]
		{
			"Unable to route due to invalid recipient address"
		});

		// Token: 0x04000071 RID: 113
		public static readonly SmtpResponse TimeoutOccurred = new SmtpResponse("451", "4.7.0", new string[]
		{
			"Timeout waiting for client input"
		});

		// Token: 0x04000072 RID: 114
		public static readonly SmtpResponse UnsupportedCommand = new SmtpResponse("550", "5.7.1", new string[]
		{
			"Command not allowed"
		});

		// Token: 0x04000073 RID: 115
		public static readonly SmtpResponse UnrecognizedCommand = new SmtpResponse("500", "5.3.3", new string[]
		{
			"Unrecognized command"
		});

		// Token: 0x04000074 RID: 116
		public static readonly SmtpResponse UnrecognizedParameter = new SmtpResponse("501", "5.5.4", new string[]
		{
			"Unrecognized parameter"
		});

		// Token: 0x04000075 RID: 117
		public static readonly SmtpResponse MailFromOk = new SmtpResponse("250", "2.1.0", new string[]
		{
			"Sender OK"
		});

		// Token: 0x04000076 RID: 118
		public static readonly SmtpResponse RcptToOk = new SmtpResponse("250", "2.1.5", new string[]
		{
			"Recipient OK"
		});

		// Token: 0x04000077 RID: 119
		internal static readonly SmtpResponse Rcpt2ToOk = new SmtpResponse("250", "2.1.5", new string[]
		{
			"Recipient2 OK"
		});

		// Token: 0x04000078 RID: 120
		internal static readonly SmtpResponse Rcpt2ToOkButInvalidAddress = new SmtpResponse("249", "2.1.5", new string[]
		{
			"Recipient2 OK but invalid address"
		});

		// Token: 0x04000079 RID: 121
		internal static readonly SmtpResponse Rcpt2ToOkButInvalidArguments = new SmtpResponse("247", "2.1.5", new string[]
		{
			"Recipient2 OK but invalid parameters"
		});

		// Token: 0x0400007A RID: 122
		internal static readonly SmtpResponse Rcpt2ToOkButRcpt2AddressDifferentFromRcptAddress = new SmtpResponse("248", "2.1.5", new string[]
		{
			"Recipient2 OK but unknown address"
		});

		// Token: 0x0400007B RID: 123
		internal static readonly SmtpResponse Rcpt2AlreadyReceived = new SmtpResponse("503", "5.5.2", new string[]
		{
			"Recipient2 already received"
		});

		// Token: 0x0400007C RID: 124
		public static readonly SmtpResponse TlsDomainRequired = new SmtpResponse("421", string.Empty, new string[]
		{
			"TLSAuthLevel cannot be DomainValidation with out a valid TLS Domain."
		});

		// Token: 0x0400007D RID: 125
		public static readonly SmtpResponse IncorrectTlsAuthLevel = new SmtpResponse("421", string.Empty, new string[]
		{
			"TLS Domain can only be specified with TLSAuthLevel set to DomainValidation."
		});

		// Token: 0x0400007E RID: 126
		public static readonly SmtpResponse ServiceInactive = new SmtpResponse("421", "4.3.2", new string[]
		{
			"Service not active"
		});

		// Token: 0x0400007F RID: 127
		public static readonly SmtpResponse SpamFilterNotReady = new SmtpResponse("421", "4.3.2", new string[]
		{
			"Temporary local error initializing data. System not accepting network messages. (SF1)"
		});

		// Token: 0x04000080 RID: 128
		public static readonly SmtpResponse InvalidMailboxSpamFilterRule = new SmtpResponse("421", "4.3.2", new string[]
		{
			"Temporary local error initializing data. System not accepting network messages. (SF2)"
		});

		// Token: 0x04000081 RID: 129
		public static readonly SmtpResponse EnvelopeFilterNotReady = new SmtpResponse("421", "4.3.2", new string[]
		{
			"Temporary local error initializing data. System not accepting IPv6 network messages. (EV1)"
		});

		// Token: 0x04000082 RID: 130
		public static readonly SmtpResponse OrgQueueQuotaExceeded = new SmtpResponse("450", "4.7.0", new string[]
		{
			"Organization queue quota exceeded."
		});

		// Token: 0x04000083 RID: 131
		internal static readonly SmtpResponse AuthAlreadySpecified = new SmtpResponse("503", "5.5.2", new string[]
		{
			"Auth command already specified"
		});

		// Token: 0x04000084 RID: 132
		internal static readonly SmtpResponse AuthCancelled = new SmtpResponse("501", "5.5.4", new string[]
		{
			"Authentication cancelled"
		});

		// Token: 0x04000085 RID: 133
		internal static readonly SmtpResponse AuthPassword = new SmtpResponse("334", string.Empty, new string[]
		{
			"UGFzc3dvcmQ6"
		});

		// Token: 0x04000086 RID: 134
		internal static readonly SmtpResponse AuthSuccessful = new SmtpResponse("235", "2.7.0", new string[]
		{
			"Authentication successful"
		});

		// Token: 0x04000087 RID: 135
		internal static readonly SmtpResponse AuthTempFailure = new SmtpResponse("454", "4.7.0", new string[]
		{
			"Temporary authentication failure"
		});

		// Token: 0x04000088 RID: 136
		internal static readonly SmtpResponse AuthorizationTempFailure = new SmtpResponse("454", "4.7.0", new string[]
		{
			"Temporary authorization failure"
		});

		// Token: 0x04000089 RID: 137
		internal static readonly SmtpResponse AuthTempFailureTLSCipherTooWeak = new SmtpResponse("454", "4.7.0", new string[]
		{
			"Temporary authentication failure because negotiated Tls cipher strength is too weak"
		});

		// Token: 0x0400008A RID: 138
		internal static readonly SmtpResponse AuthUnrecognized = new SmtpResponse("504", "5.7.4", new string[]
		{
			"Unrecognized authentication type"
		});

		// Token: 0x0400008B RID: 139
		internal static readonly SmtpResponse AuthUnsuccessful = new SmtpResponse("535", "5.7.3", new string[]
		{
			"Authentication unsuccessful"
		});

		// Token: 0x0400008C RID: 140
		internal static readonly SmtpResponse SubmissionDisabledBySendQuota = new SmtpResponse("550", "5.7.1", new string[]
		{
			"Submission has been disabled for this account"
		});

		// Token: 0x0400008D RID: 141
		internal static readonly SmtpResponse CryptographicFailure = new SmtpResponse("554", "5.7.5", new string[]
		{
			"Cryptographic failure"
		});

		// Token: 0x0400008E RID: 142
		internal static readonly SmtpResponse CertificateValidationFailure = new SmtpResponse("454", "4.7.5", new string[]
		{
			"Certificate validation failure"
		});

		// Token: 0x0400008F RID: 143
		internal static readonly SmtpResponse AuthUsername = new SmtpResponse("334", string.Empty, new string[]
		{
			"VXNlcm5hbWU6"
		});

		// Token: 0x04000090 RID: 144
		internal static readonly SmtpResponse CommandTooLong = new SmtpResponse("500", "5.5.2", new string[]
		{
			"Command too long"
		});

		// Token: 0x04000091 RID: 145
		internal static readonly SmtpResponse ConnectionDropped = new SmtpResponse("421", "4.4.2", new string[]
		{
			"Connection dropped"
		});

		// Token: 0x04000092 RID: 146
		internal static readonly SmtpResponse CommandNotImplemented = new SmtpResponse("502", "5.3.3", new string[]
		{
			"Command not implemented"
		});

		// Token: 0x04000093 RID: 147
		internal static readonly SmtpResponse MessageSizeLimitReached = new SmtpResponse("421", "4.4.2", new string[]
		{
			"Message size limit for this connection reached"
		});

		// Token: 0x04000094 RID: 148
		internal static readonly SmtpResponse MessageRateLimitExceeded = new SmtpResponse("421", "4.4.2", new string[]
		{
			"Message submission rate for this client has exceeded the configured limit"
		});

		// Token: 0x04000095 RID: 149
		internal static readonly SmtpResponse MessageDeletedByAdmin = new SmtpResponse("421", "4.3.2", new string[]
		{
			"System not accepting network messages"
		});

		// Token: 0x04000096 RID: 150
		internal static readonly SmtpResponse RetryDataSend = new SmtpResponse("451", "4.3.0", new string[]
		{
			"I/O error reading message body"
		});

		// Token: 0x04000097 RID: 151
		internal static readonly SmtpResponse ShadowRedundancyFailed = new SmtpResponse("451", "4.4.0", new string[]
		{
			"Message failed to be made redundant"
		});

		// Token: 0x04000098 RID: 152
		internal static readonly SmtpResponse HeadersTooLarge = new SmtpResponse("552", "5.3.4", new string[]
		{
			"Header size exceeds fixed maximum size"
		});

		// Token: 0x04000099 RID: 153
		internal static readonly SmtpResponse Help = new SmtpResponse("214", string.Empty, new string[]
		{
			"This server supports the following commands:",
			"HELO EHLO STARTTLS RCPT DATA RSET MAIL QUIT HELP AUTH BDAT"
		});

		// Token: 0x0400009A RID: 154
		internal static readonly SmtpResponse HopCountExceeded = new SmtpResponse("554", "5.4.6", new string[]
		{
			"Hop count exceeded - possible mail loop"
		});

		// Token: 0x0400009B RID: 155
		internal static readonly SmtpResponse ProxyHopCountExceeded = new SmtpResponse("554", "5.4.6", new string[]
		{
			"Proxy Hop count exceeded"
		});

		// Token: 0x0400009C RID: 156
		internal static readonly SmtpResponse AgentGeneratedMessageDepthExceeded = new SmtpResponse("554", "5.3.5", new string[]
		{
			"Agent generated message depth exceeded"
		});

		// Token: 0x0400009D RID: 157
		internal static readonly SmtpResponse InvalidEhloDomain = new SmtpResponse("501", "5.5.4", new string[]
		{
			"Invalid domain name"
		});

		// Token: 0x0400009E RID: 158
		internal static readonly SmtpResponse InvalidHeloDomain = new SmtpResponse("501", "5.5.4", new string[]
		{
			"Invalid domain name"
		});

		// Token: 0x0400009F RID: 159
		internal static readonly SmtpResponse EhloDomainRequired = new SmtpResponse("501", "5.0.0", new string[]
		{
			"EHLO requires domain address"
		});

		// Token: 0x040000A0 RID: 160
		internal static readonly SmtpResponse HeloDomainRequired = new SmtpResponse("501", "5.0.0", new string[]
		{
			"HELO requires domain address"
		});

		// Token: 0x040000A1 RID: 161
		internal static readonly SmtpResponse XShadowContextRequired = new SmtpResponse("501", "5.0.0", new string[]
		{
			"XSHADOW requires Shadow server's context"
		});

		// Token: 0x040000A2 RID: 162
		internal static readonly SmtpResponse XQDiscardMaxDiscardCountRequired = new SmtpResponse("501", "5.0.0", new string[]
		{
			"XQDISCARD requires maximum discard events count"
		});

		// Token: 0x040000A3 RID: 163
		internal static readonly SmtpResponse MailFromAlreadySpecified = new SmtpResponse("503", "5.5.2", new string[]
		{
			"Sender already specified"
		});

		// Token: 0x040000A4 RID: 164
		internal static readonly SmtpResponse MailingListExpansionProblem = new SmtpResponse("550", "5.2.4", new string[]
		{
			"Mailing list expansion problem"
		});

		// Token: 0x040000A5 RID: 165
		internal static readonly SmtpResponse MailboxDisabled = new SmtpResponse("550", "5.2.1", new string[]
		{
			"Mailbox disabled, not accepting messages"
		});

		// Token: 0x040000A6 RID: 166
		internal static readonly SmtpResponse MailboxOffline = new SmtpResponse("550", "5.2.1", new string[]
		{
			"Mailbox cannot be accessed"
		});

		// Token: 0x040000A7 RID: 167
		internal static readonly SmtpResponse MailboxOverQuota = new SmtpResponse("550", "5.2.2", new string[]
		{
			"Mailbox full"
		});

		// Token: 0x040000A8 RID: 168
		internal static readonly SmtpResponse SubmissionQuotaExceeded = new SmtpResponse("550", "5.2.2", new string[]
		{
			"Submission quota exceeded"
		});

		// Token: 0x040000A9 RID: 169
		internal static readonly SmtpResponse NeedStartTls = new SmtpResponse("530", "5.7.0", new string[]
		{
			"Must issue a STARTTLS command first"
		});

		// Token: 0x040000AA RID: 170
		internal static readonly SmtpResponse NeedHello = new SmtpResponse("503", "5.5.2", new string[]
		{
			"Send hello first"
		});

		// Token: 0x040000AB RID: 171
		internal static readonly SmtpResponse NeedEhlo = new SmtpResponse("503", "5.5.2", new string[]
		{
			"Send EHLO first"
		});

		// Token: 0x040000AC RID: 172
		internal static readonly SmtpResponse NeedMailFrom = new SmtpResponse("503", "5.5.2", new string[]
		{
			"Need mail command"
		});

		// Token: 0x040000AD RID: 173
		internal static readonly SmtpResponse NeedRcptTo = new SmtpResponse("503", "5.5.2", new string[]
		{
			"Need rcpt command"
		});

		// Token: 0x040000AE RID: 174
		internal static readonly SmtpResponse NoRecipientSucceeded = new SmtpResponse("450", "4.5.0", new string[]
		{
			"No recipient succeeded"
		});

		// Token: 0x040000AF RID: 175
		internal static readonly SmtpResponse NotAuthenticated = new SmtpResponse("530", "5.7.1", new string[]
		{
			"Not authenticated"
		});

		// Token: 0x040000B0 RID: 176
		internal static readonly SmtpResponse NotAuthorized = new SmtpResponse("550", "5.7.1", new string[]
		{
			"Not authorized"
		});

		// Token: 0x040000B1 RID: 177
		internal static readonly SmtpResponse Quit = new SmtpResponse("221", "2.0.0", new string[]
		{
			"Service closing transmission channel"
		});

		// Token: 0x040000B2 RID: 178
		internal static readonly SmtpResponse RcptRelayNotPermitted = new SmtpResponse("550", "5.7.1", new string[]
		{
			"Unable to relay"
		});

		// Token: 0x040000B3 RID: 179
		internal static readonly SmtpResponse Reset = new SmtpResponse("250", "2.0.0", new string[]
		{
			"Resetting"
		});

		// Token: 0x040000B4 RID: 180
		internal static readonly SmtpResponse XSessionParamsOk = new SmtpResponse("250", "2.0.0", new string[]
		{
			"XSESSIONPARAMS OK"
		});

		// Token: 0x040000B5 RID: 181
		internal static readonly SmtpResponse DomainSecureDisabled = new SmtpResponse("451", "4.7.3", new string[]
		{
			"The admin has temporarily disallowed this secure domain"
		});

		// Token: 0x040000B6 RID: 182
		internal static readonly SmtpResponse RequireTLSToSendMail = new SmtpResponse("451", "5.7.3", new string[]
		{
			"STARTTLS is required to send mail"
		});

		// Token: 0x040000B7 RID: 183
		internal static readonly SmtpResponse RequireBasicAuthentication = new SmtpResponse("451", "5.7.3", new string[]
		{
			"Require basic authentication"
		});

		// Token: 0x040000B8 RID: 184
		internal static readonly SmtpResponse RequireSTARTTLSToAuth = new SmtpResponse("451", "5.7.3", new string[]
		{
			"Require STARTTLS to authenticate"
		});

		// Token: 0x040000B9 RID: 185
		internal static readonly SmtpResponse RequireSTARTTLSToBasicAuth = new SmtpResponse("451", "5.7.3", new string[]
		{
			"Require STARTTLS to do basic authentication"
		});

		// Token: 0x040000BA RID: 186
		internal static readonly SmtpResponse RequireXOorgToSendMail = new SmtpResponse("451", "5.7.3", new string[]
		{
			"Require XOORG extension to send mail"
		});

		// Token: 0x040000BB RID: 187
		internal static readonly SmtpResponse CannotExchangeAuthenticate = new SmtpResponse("451", "5.7.3", new string[]
		{
			"Cannot achieve Exchange Server authentication"
		});

		// Token: 0x040000BC RID: 188
		internal static readonly SmtpResponse InternalTransportCertificateNotAvailable = new SmtpResponse("451", "5.7.3", new string[]
		{
			"Cannot load Internal Transport Certificate. Exchange Server authentication failed."
		});

		// Token: 0x040000BD RID: 189
		internal static readonly SmtpResponse StartData = new SmtpResponse("354", string.Empty, new string[]
		{
			"Start mail input; end with <CRLF>.<CRLF>"
		});

		// Token: 0x040000BE RID: 190
		internal static readonly SmtpResponse StartTlsAlreadyNegotiated = new SmtpResponse("503", "5.5.2", new string[]
		{
			"Only one STARTTLS command can be issued per session"
		});

		// Token: 0x040000BF RID: 191
		internal static readonly SmtpResponse StartTlsNegotiationFailure = new SmtpResponse("554", "5.7.3", new string[]
		{
			"Unable to initialize security subsystem"
		});

		// Token: 0x040000C0 RID: 192
		internal static readonly SmtpResponse StartTlsReadyToNegotiate = new SmtpResponse("220", "2.0.0", new string[]
		{
			"SMTP server ready"
		});

		// Token: 0x040000C1 RID: 193
		internal static readonly SmtpResponse StartTlsTempReject = new SmtpResponse("454", "4.7.0", new string[]
		{
			"Cannot accept TLS as maximum TLS rate exceeded for server"
		});

		// Token: 0x040000C2 RID: 194
		internal static readonly SmtpResponse TooManyAuthenticationErrors = new SmtpResponse("421", "4.7.0", new string[]
		{
			"Too many errors on this connection, closing transmission channel"
		});

		// Token: 0x040000C3 RID: 195
		internal static readonly SmtpResponse TooManyProtocolErrors = new SmtpResponse("421", "4.7.0", new string[]
		{
			"Too many errors on this connection, closing transmission channel"
		});

		// Token: 0x040000C4 RID: 196
		internal static readonly SmtpResponse TooManyConnections = new SmtpResponse("421", "4.3.2", new string[]
		{
			"The maximum number of concurrent server connections has exceeded a limit, closing transmission channel"
		});

		// Token: 0x040000C5 RID: 197
		internal static readonly SmtpResponse TooManyConnectionsPerSource = new SmtpResponse("421", "4.3.2", new string[]
		{
			"The maximum number of concurrent connections has exceeded a limit, closing transmission channel"
		});

		// Token: 0x040000C6 RID: 198
		internal static readonly SmtpResponse TooManyRecipients = new SmtpResponse("452", "4.5.3", new string[]
		{
			"Too many recipients"
		});

		// Token: 0x040000C7 RID: 199
		internal static readonly SmtpResponse MessageExpired = new SmtpResponse("500", "4.4.7", new string[]
		{
			"Message expired"
		});

		// Token: 0x040000C8 RID: 200
		internal static readonly SmtpResponse MessageDelayed = new SmtpResponse("400", "4.4.7", new string[]
		{
			"Message delayed"
		});

		// Token: 0x040000C9 RID: 201
		internal static readonly SmtpResponse MessageDelayedConcurrentDeliveries = new SmtpResponse("400", "4.4.7", new string[]
		{
			"Message delayed, too many concurrent deliveries at this time"
		});

		// Token: 0x040000CA RID: 202
		internal static readonly SmtpResponse UnableToConnect = new SmtpResponse("421", "4.2.1", new string[]
		{
			"Unable to connect"
		});

		// Token: 0x040000CB RID: 203
		internal static readonly SmtpResponse UnsupportedBodyType = new SmtpResponse("501", "5.5.4", new string[]
		{
			"Unsupported BODY type"
		});

		// Token: 0x040000CC RID: 204
		internal static readonly SmtpResponse Xexch50Success = new SmtpResponse("250", "2.0.0", new string[]
		{
			"XEXCH50 OK"
		});

		// Token: 0x040000CD RID: 205
		internal static readonly SmtpResponse Xexch50SendBlob = new SmtpResponse("354", string.Empty, new string[]
		{
			"Send binary data"
		});

		// Token: 0x040000CE RID: 206
		internal static readonly SmtpResponse Xexch50Error = new SmtpResponse("500", "5.5.2", new string[]
		{
			"Error processing XEXCH50 command"
		});

		// Token: 0x040000CF RID: 207
		internal static readonly SmtpResponse XMessageContextError = new SmtpResponse("400", "4.5.1", new string[]
		{
			"Error processing BDAT blob with Message context information"
		});

		// Token: 0x040000D0 RID: 208
		internal static readonly SmtpResponse XMessageEPropNotFoundInMailCommand = new SmtpResponse("400", "4.5.2", new string[]
		{
			"Did not receive mandatory Extended Properties XMESSAGECONTEXT blob in MAIL command"
		});

		// Token: 0x040000D1 RID: 209
		internal static readonly SmtpResponse Xexch50NotEnabled = new SmtpResponse("500", "5.5.1", new string[]
		{
			"Not enabled"
		});

		// Token: 0x040000D2 RID: 210
		internal static readonly SmtpResponse Xexch50InvalidCommand = new SmtpResponse("501", "5.5.4", new string[]
		{
			"XEXCH50 Invalid command format"
		});

		// Token: 0x040000D3 RID: 211
		internal static readonly SmtpResponse Xexch50NotAuthorized = new SmtpResponse("504", "5.7.1", new string[]
		{
			"Not authorized"
		});

		// Token: 0x040000D4 RID: 212
		internal static readonly SmtpResponse OrarNotAuthorized = new SmtpResponse("504", "5.7.1", new string[]
		{
			"Not authorized to send ORAR"
		});

		// Token: 0x040000D5 RID: 213
		internal static readonly SmtpResponse RDstNotAuthorized = new SmtpResponse("504", "5.7.1", new string[]
		{
			"Not authorized to send RDST"
		});

		// Token: 0x040000D6 RID: 214
		internal static readonly SmtpResponse UnableToAcceptAnonymousSession = new SmtpResponse("530", "5.7.1", new string[]
		{
			"Client was not authenticated"
		});

		// Token: 0x040000D7 RID: 215
		internal static readonly SmtpResponse RoutingLoopDetected = new SmtpResponse("500", "5.4.6", new string[]
		{
			"Routing Loop Detected"
		});

		// Token: 0x040000D8 RID: 216
		internal static readonly SmtpResponse UnableToVrfyUser = new SmtpResponse("252", "2.1.5", new string[]
		{
			"Cannot VRFY user"
		});

		// Token: 0x040000D9 RID: 217
		internal static readonly SmtpResponse LongAddress = new SmtpResponse("501", "5.1.3", new string[]
		{
			"Long addresses not supported"
		});

		// Token: 0x040000DA RID: 218
		internal static readonly SmtpResponse SmtpUtf8ArgumentNotProvided = new SmtpResponse("501", "5.1.6", new string[]
		{
			"SMTPUTF8 argument required."
		});

		// Token: 0x040000DB RID: 219
		internal static readonly SmtpResponse LongSenderAddress = new SmtpResponse("501", "5.1.7", new string[]
		{
			"Long addresses not supported"
		});

		// Token: 0x040000DC RID: 220
		internal static readonly SmtpResponse Utf8Address = new SmtpResponse("501", "5.1.8", new string[]
		{
			"UTF-8 addresses not supported"
		});

		// Token: 0x040000DD RID: 221
		internal static readonly SmtpResponse Utf8SenderAddress = new SmtpResponse("501", "5.1.9", new string[]
		{
			"UTF-8 addresses not supported"
		});

		// Token: 0x040000DE RID: 222
		internal static readonly SmtpResponse NtlmSupported = new SmtpResponse("334", string.Empty, new string[]
		{
			"NTLM supported"
		});

		// Token: 0x040000DF RID: 223
		internal static readonly SmtpResponse GssapiSupported = new SmtpResponse("334", string.Empty, new string[]
		{
			"GSSAPI supported"
		});

		// Token: 0x040000E0 RID: 224
		internal static readonly SmtpResponse QueueSuspended = new SmtpResponse("400", "4.0.0", new string[]
		{
			"Message delivery delayed due to a suspended delivery queue"
		});

		// Token: 0x040000E1 RID: 225
		internal static readonly SmtpResponse QueueLarge = new SmtpResponse("400", "4.0.0", new string[]
		{
			"Message delivery delayed due to a large queue"
		});

		// Token: 0x040000E2 RID: 226
		internal static readonly SmtpResponse InterceptorPermanentlyRejectedMessage = new SmtpResponse("530", "5.3.1", new string[]
		{
			"Intercepted and rejected by administrative action. PRJCT"
		});

		// Token: 0x040000E3 RID: 227
		internal static readonly SmtpResponse InterceptorTransientlyRejectedMessage = new SmtpResponse("430", "4.3.1", new string[]
		{
			"Intercepted and rejected by administrative action. TRJCT"
		});

		// Token: 0x040000E4 RID: 228
		internal static readonly SmtpResponse AgentDiscardedMessage = new SmtpResponse("550", "4.3.2", new string[]
		{
			"Discarded by administrative action. DROP"
		});

		// Token: 0x040000E5 RID: 229
		internal static readonly SmtpResponse TooManyRelatedErrors = new SmtpResponse("530", "5.3.0", new string[]
		{
			"Too many related errors"
		});

		// Token: 0x040000E6 RID: 230
		internal static readonly SmtpResponse RecipientDeferredNoMdb = new SmtpResponse("420", "4.2.0", new string[]
		{
			"Recipient deferred because there is no Mdb"
		});

		// Token: 0x040000E7 RID: 231
		internal static readonly SmtpResponse ProbeMessageDropped = new SmtpResponse("250", string.Empty, new string[]
		{
			"Probe message accepted and dropped"
		});

		// Token: 0x040000E8 RID: 232
		internal static readonly SmtpResponse SocketError = new SmtpResponse("441", "4.4.1", new string[]
		{
			"Socket error"
		});

		// Token: 0x040000E9 RID: 233
		internal static readonly SmtpResponse ConnectionFailover = new SmtpResponse("451", "4.4.0", new string[]
		{
			"Connection failover"
		});

		// Token: 0x040000EA RID: 234
		internal static readonly SmtpResponse HubAttributionFailureInEOH = new SmtpResponse("550", "5.7.1", new string[]
		{
			"Unable to Relay. ATTR11"
		});

		// Token: 0x040000EB RID: 235
		internal static readonly SmtpResponse HubAttributionTransientFailureInEOH = new SmtpResponse("450", "4.7.1", new string[]
		{
			"Unable to fetch attribution data. ATTR11"
		});

		// Token: 0x040000EC RID: 236
		internal static readonly SmtpResponse ProxyAttributionFailureInEOH = new SmtpResponse("550", "5.7.1", new string[]
		{
			"Unable to Relay. ATTR21"
		});

		// Token: 0x040000ED RID: 237
		internal static readonly SmtpResponse ProxyAttributionTransientFailureinEOH = new SmtpResponse("450", "4.7.1", new string[]
		{
			"Unable to fetch attribution data. ATTR21"
		});

		// Token: 0x040000EE RID: 238
		internal static readonly SmtpResponse HubAttributionTransientFailureInEOHFallback = new SmtpResponse("450", "4.7.1", new string[]
		{
			"Unable to fetch attribution data. ATTR13"
		});

		// Token: 0x040000EF RID: 239
		internal static readonly SmtpResponse ProxyAttributionTransientFailureInEOHFallback = new SmtpResponse("450", "4.7.1", new string[]
		{
			"Unable to fetch attribution data. ATTR23"
		});

		// Token: 0x040000F0 RID: 240
		internal static readonly SmtpResponse InboundProxyDestinationTrackerReject = new SmtpResponse("421", "4.3.2", new string[]
		{
			"The maximum number of concurrent server connections has exceeded a limit, closing transmission channel PRX7"
		});

		// Token: 0x040000F1 RID: 241
		internal static readonly SmtpResponse HubRecipientCacheCreationFailureInEOH = new SmtpResponse("550", "5.7.1", new string[]
		{
			"Unable to Relay. ATTR12"
		});

		// Token: 0x040000F2 RID: 242
		internal static readonly SmtpResponse HubRecipientCacheCreationTransientFailureInEOH = new SmtpResponse("450", "4.7.1", new string[]
		{
			"Unable to Relay. ATTR12"
		});

		// Token: 0x040000F3 RID: 243
		internal static readonly SmtpResponse ProxyRecipientCacheCreationFailureInEOH = new SmtpResponse("550", "5.7.1", new string[]
		{
			"Unable to Relay. ATTR22"
		});

		// Token: 0x040000F4 RID: 244
		internal static readonly SmtpResponse ProxyRecipientCacheCreationTransientFailureInEOH = new SmtpResponse("450", "4.7.1", new string[]
		{
			"Unable to Relay. ATTR22"
		});

		// Token: 0x040000F5 RID: 245
		internal static readonly SmtpResponse HubAttributionFailureInMailFrom = new SmtpResponse("550", "5.7.1", new string[]
		{
			"Unable to Relay. ATTR14"
		});

		// Token: 0x040000F6 RID: 246
		internal static readonly SmtpResponse HubAttributionTransientFailureInMailFrom = new SmtpResponse("450", "4.7.1", new string[]
		{
			"Unable to Relay. ATTR14"
		});

		// Token: 0x040000F7 RID: 247
		internal static readonly SmtpResponse HubAttributionFailureInCreateTmi = new SmtpResponse("550", "5.7.1", new string[]
		{
			"Unable to Relay. ATTR15"
		});

		// Token: 0x040000F8 RID: 248
		internal static readonly SmtpResponse HubAttributionTransientFailureInCreateTmi = new SmtpResponse("450", "4.7.1", new string[]
		{
			"Unable to Relay. ATTR15"
		});

		// Token: 0x040000F9 RID: 249
		internal static readonly SmtpResponse RequiredArgumentsNotPresent = new SmtpResponse("501", "5.5.4", new string[]
		{
			"Required arguments not present"
		});

		// Token: 0x040000FA RID: 250
		internal static readonly SmtpResponse UserLookupFailed = new SmtpResponse("250", null, new string[]
		{
			"XProxy accepted but user name could not be resolved"
		});

		// Token: 0x040000FB RID: 251
		internal static readonly SmtpResponse UnableToObtainIdentity = new SmtpResponse("250", null, new string[]
		{
			"XProxy accepted but user identity could not be obtained"
		});

		// Token: 0x040000FC RID: 252
		internal static readonly SmtpResponse XProxyAcceptedAuthenticated = new SmtpResponse("250", null, new string[]
		{
			"XProxy accepted and authenticated"
		});

		// Token: 0x040000FD RID: 253
		internal static readonly SmtpResponse XProxyAccepted = new SmtpResponse("250", null, new string[]
		{
			"XProxy accepted"
		});

		// Token: 0x040000FE RID: 254
		internal static readonly SmtpResponse NoDestinationsReceivedResponse = new SmtpResponse("451", "4.5.0", new string[]
		{
			"Destinations need to be sent for proxying"
		});

		// Token: 0x040000FF RID: 255
		internal static readonly SmtpResponse SuccessfulResponse = new SmtpResponse("250", null, new string[]
		{
			"XProxyTo accepted"
		});

		// Token: 0x04000100 RID: 256
		internal static readonly SmtpResponse GenericProxyFailure = new SmtpResponse("451", "4.7.0", new string[]
		{
			"Temporary server error. Please try again later"
		});

		// Token: 0x04000101 RID: 257
		internal static readonly SmtpResponse ProxyDiscardingMessage = new SmtpResponse("451", "4.7.0", new string[]
		{
			"The proxy layer is discarding data"
		});

		// Token: 0x04000102 RID: 258
		internal static readonly SmtpResponse XProxyAlreadySpecified = new SmtpResponse("503", "5.5.0", new string[]
		{
			"XProxy already sent"
		});

		// Token: 0x04000103 RID: 259
		internal static readonly SmtpResponse UnableToProxyIntegratedAuthResponse = new SmtpResponse("535", "5.7.3", new string[]
		{
			"Unable to proxy authenticated session because either the backend does not support it or failed to resolve the user"
		});

		// Token: 0x04000104 RID: 260
		internal static readonly SmtpResponse EncodedProxyFailureResponseNoDestinations = new SmtpResponse("451", "4.7.0", new string[]
		{
			"Temporary server error. Please try again later. PRX1 "
		});

		// Token: 0x04000105 RID: 261
		internal static readonly SmtpResponse EncodedProxyFailureResponseDnsError = new SmtpResponse("451", "4.7.0", new string[]
		{
			"Temporary server error. Please try again later. PRX2 "
		});

		// Token: 0x04000106 RID: 262
		internal static readonly SmtpResponse EncodedProxyFailureResponseConnectionFailure = new SmtpResponse("451", "4.7.0", new string[]
		{
			"Temporary server error. Please try again later. PRX3 "
		});

		// Token: 0x04000107 RID: 263
		internal static readonly SmtpResponse EncodedProxyFailureResponseProtocolError = new SmtpResponse("451", "4.7.0", new string[]
		{
			"Temporary server error. Please try again later. PRX4 "
		});

		// Token: 0x04000108 RID: 264
		internal static readonly SmtpResponse EncodedProxyFailureResponseSocketError = new SmtpResponse("451", "4.7.0", new string[]
		{
			"Temporary server error. Please try again later. PRX5 "
		});

		// Token: 0x04000109 RID: 265
		internal static readonly SmtpResponse EncodedProxyFailureResponseShutdown = new SmtpResponse("451", "4.7.0", new string[]
		{
			"Temporary server error. Please try again later. PRX6 "
		});

		// Token: 0x0400010A RID: 266
		internal static readonly SmtpResponse EncodedProxyFailureResponseBackEndLocatorFailure = new SmtpResponse("451", "4.7.0", new string[]
		{
			"Temporary server error. Please try again later. CPRX1 "
		});

		// Token: 0x0400010B RID: 267
		internal static readonly SmtpResponse EncodedProxyFailureResponseUserLookupFailure = new SmtpResponse("451", "4.7.0", new string[]
		{
			"Temporary server error. Please try again later. CPRX2 "
		});

		// Token: 0x0400010C RID: 268
		internal static readonly SmtpResponse AuthenticationFailureTenantLockedOut = new SmtpResponse("535", "5.7.3", new string[]
		{
			"Authentication unsuccessful. Tenant locked out"
		});

		// Token: 0x0400010D RID: 269
		internal static readonly SmtpResponse AuthenticationFailureUserNotFound = new SmtpResponse("535", "5.7.3", new string[]
		{
			"Authentication unsuccessful. User not found"
		});

		// Token: 0x0400010E RID: 270
		internal static readonly SmtpResponse NoProxyDestinationsResponse = new SmtpResponse("451", "4.7.0", new string[]
		{
			"No proxy destinations could be obtained"
		});

		// Token: 0x0400010F RID: 271
		internal static readonly SmtpResponse SuccessNoNewConnectionResponse = new SmtpResponse("250", null, new string[]
		{
			"Success; no new connection requested"
		});

		// Token: 0x04000110 RID: 272
		internal static readonly SmtpResponse MessageNotProxiedResponse = new SmtpResponse("450", "4.7.0", new string[]
		{
			"Message not proxied"
		});

		// Token: 0x04000111 RID: 273
		internal static readonly SmtpResponse ProxySessionProtocolSetupPermanentFailure = new SmtpResponse("550", "5.7.0", new string[]
		{
			"Proxy session setup failed on Frontend with "
		});

		// Token: 0x04000112 RID: 274
		internal static readonly SmtpResponse ProxySessionProtocolSetupTransientFailure = new SmtpResponse("450", "4.7.0", new string[]
		{
			"Proxy session setup failed on Frontend with "
		});

		// Token: 0x04000113 RID: 275
		internal static readonly SmtpResponse GssapiSmtpResponseToLog334 = new SmtpResponse("334", null, new string[]
		{
			"<authentication information>"
		});

		// Token: 0x04000114 RID: 276
		internal static readonly SmtpResponse GssapiSmtpResponseToLog235 = new SmtpResponse("235", null, new string[]
		{
			"<authentication information>"
		});

		// Token: 0x04000115 RID: 277
		internal static readonly SmtpResponse RequireXProxy = new SmtpResponse("451", "4.5.0", new string[]
		{
			"Require XPROXY"
		});

		// Token: 0x04000116 RID: 278
		internal static readonly SmtpResponse RequireXProxyTo = new SmtpResponse("451", "4.5.0", new string[]
		{
			"Require XPROXYTO"
		});

		// Token: 0x04000117 RID: 279
		internal static readonly SmtpResponse RequireMatchingEhloOptions = new SmtpResponse("451", "4.5.0", new string[]
		{
			"Require EHLO options to match"
		});

		// Token: 0x04000118 RID: 280
		internal static readonly SmtpResponse RequireEhloToSendMail = new SmtpResponse("451", "4.5.0", new string[]
		{
			"Require EHLO to send mail"
		});

		// Token: 0x04000119 RID: 281
		internal static readonly SmtpResponse RequireAnonymousTlsToSendMail = new SmtpResponse("451", "4.5.0", new string[]
		{
			"Require XAnonymousTls to send mail"
		});

		// Token: 0x0400011A RID: 282
		internal static readonly SmtpResponse EhloOptionsDoNotMatchForProxy = new SmtpResponse("451", "4.7.0", new string[]
		{
			"EHLO Options do not match for proxy"
		});

		// Token: 0x0400011B RID: 283
		internal static readonly SmtpResponse XProxyFromAccepted = new SmtpResponse("250", null, new string[]
		{
			"XProxyFrom accepted"
		});

		// Token: 0x0400011C RID: 284
		internal static readonly SmtpResponse DnsQueryFailedResponseDefault = new SmtpResponse("451", "4.4.0", new string[]
		{
			"DNS query failed"
		});

		// Token: 0x0400011D RID: 285
		internal static readonly SmtpResponse DnsConfigChangedSmtpResponse = new SmtpResponse("554", "5.4.4", new string[]
		{
			"Configuration changed"
		});

		// Token: 0x0400011E RID: 286
		internal static readonly SmtpResponse UnexpectedExceptionHandlingNewOutboundConnection = new SmtpResponse("421", null, new string[]
		{
			"Unexpected exception occurred when trying to handle a new SMTP outbound connection."
		});

		// Token: 0x0400011F RID: 287
		internal static readonly SmtpResponse XProxyToRequired = new SmtpResponse("451", "4.7.0", new string[]
		{
			"XPROXYTO is required to send mail "
		});

		// Token: 0x04000120 RID: 288
		internal static readonly SmtpResponse AuthenticationFailedTemporary = new SmtpResponse("451", "4.7.3", new string[]
		{
			"Authentication unsuccessful due to temporary server error. Please try again later."
		});

		// Token: 0x04000121 RID: 289
		internal static readonly SmtpResponse AuthenticationFailedPermanent = new SmtpResponse("535", "5.7.3", new string[]
		{
			"Authentication unsuccessful due to permanent server error."
		});

		// Token: 0x04000122 RID: 290
		internal static readonly SmtpResponse CheckTenantLockedOutFailedTemporary = new SmtpResponse("451", "4.7.3", new string[]
		{
			"Checking tenant locked out state unsuccessful due to temporary server error. Please try again later."
		});

		// Token: 0x04000123 RID: 291
		internal static readonly SmtpResponse CheckTenantLockedOutFailedPermanent = new SmtpResponse("535", "5.7.3", new string[]
		{
			"Checking tenant locked out state unsuccessful due to permanent server error."
		});

		// Token: 0x04000124 RID: 292
		private readonly bool empty;

		// Token: 0x04000125 RID: 293
		private string[] data;

		// Token: 0x04000126 RID: 294
		private string[] statusText;

		// Token: 0x04000127 RID: 295
		private byte[] bytes;

		// Token: 0x02000025 RID: 37
		internal struct TenantAttribution
		{
			// Token: 0x060000D0 RID: 208 RVA: 0x00005654 File Offset: 0x00003854
			public static SmtpResponse GetInvalidChannelRejectResponse(string failureReason)
			{
				string text;
				if (string.IsNullOrEmpty(failureReason))
				{
					text = "Failed to establish appropriate TLS channel: Access Denied";
				}
				else
				{
					text = string.Format("Failed to establish appropriate TLS channel: {0}: Access Denied", failureReason);
				}
				return new SmtpResponse("454", "4.7.0", new string[]
				{
					text
				});
			}

			// Token: 0x04000129 RID: 297
			public static readonly SmtpResponse UnattributableMailRejectSmtpResponse = new SmtpResponse("550", "5.7.0", new string[]
			{
				"Relay Access Denied"
			});

			// Token: 0x0400012A RID: 298
			public static readonly SmtpResponse RelayNotAllowedRejectSmtpResponse = new SmtpResponse("550", "5.7.1", new string[]
			{
				"Relay Access Denied ATTR1"
			});

			// Token: 0x0400012B RID: 299
			public static readonly SmtpResponse UnAuthorizedMessageOverIPv6 = new SmtpResponse("550", "5.7.1", new string[]
			{
				"Unable to accept unauthorized message over IPv6 ATTR2"
			});

			// Token: 0x0400012C RID: 300
			public static readonly SmtpResponse RecipientBelongsToDifferentDomainThanPreviouslyAttributedRejectSmtpResponse = new SmtpResponse("452", "4.5.3", new string[]
			{
				"Too many recipients"
			});

			// Token: 0x0400012D RID: 301
			public static readonly SmtpResponse MissingAttributionHeaderSmtpResponse = new SmtpResponse("451", "4.4.0", new string[]
			{
				"Temporary server error. Please try again later"
			});

			// Token: 0x0400012E RID: 302
			public static readonly SmtpResponse AppConfigFfoHubMissingSmtpResponse = new SmtpResponse("451", "4.4.4", new string[]
			{
				"Temporary server error. Please try again later ATTR1"
			});

			// Token: 0x0400012F RID: 303
			public static readonly SmtpResponse ExoSmtpNextHopDomainMissingForHostedCustomerSmtpResponse = new SmtpResponse("451", "4.4.4", new string[]
			{
				"Temporary server error. Please try again later ATTR2"
			});

			// Token: 0x04000130 RID: 304
			public static readonly SmtpResponse GlsMissingTenantPropertiesSmtpResponse = new SmtpResponse("451", "4.4.4", new string[]
			{
				"Temporary server error. Please try again later ATTR3"
			});

			// Token: 0x04000131 RID: 305
			public static readonly SmtpResponse UnknownCustomerTypeSmtpResponse = new SmtpResponse("451", "4.4.4", new string[]
			{
				"Temporary server error. Please try again later ATTR5"
			});

			// Token: 0x04000132 RID: 306
			public static readonly SmtpResponse DirectoryRequestOverThresholdSmtpResponse = new SmtpResponse("451", "4.3.2", new string[]
			{
				"Temporary server error. Please try again later ATTR1"
			});

			// Token: 0x04000133 RID: 307
			public static readonly SmtpResponse DirectoryRequestFailureSmtpResponse = new SmtpResponse("451", "4.4.3", new string[]
			{
				"Temporary server error. Please try again later ATTR1"
			});

			// Token: 0x04000134 RID: 308
			public static readonly SmtpResponse GlsRequestOverThresholdSmtpResponse = new SmtpResponse("451", "4.3.2", new string[]
			{
				"Temporary server error. Please try again later ATTR2"
			});

			// Token: 0x04000135 RID: 309
			public static readonly SmtpResponse GlsRequestErrorSmtpResponse = new SmtpResponse("451", "4.4.3", new string[]
			{
				"Temporary server error. Please try again later ATTR2"
			});

			// Token: 0x04000136 RID: 310
			public static readonly SmtpResponse DefaultDomainQueryErrorSmtpResponse = new SmtpResponse("451", "4.4.3", new string[]
			{
				"Temporary server error. Please try again later ATTR3.1"
			});

			// Token: 0x04000137 RID: 311
			public static readonly SmtpResponse DefaultDomainQueryErrorInEOHSmtpResponse = new SmtpResponse("451", "4.4.3", new string[]
			{
				"Temporary server error. Please try again later ATTR3.2"
			});

			// Token: 0x04000138 RID: 312
			public static readonly SmtpResponse UpdateScopeAndDirectoryFailureSmtpResponse = new SmtpResponse("451", "4.4.3", new string[]
			{
				"Temporary server error. Please try again later ATTR6"
			});

			// Token: 0x04000139 RID: 313
			public static readonly SmtpResponse HopCountExceededAttribution = new SmtpResponse("554", "5.4.6", new string[]
			{
				"Hop count exceeded - possible mail loop ATTR1"
			});
		}
	}
}
