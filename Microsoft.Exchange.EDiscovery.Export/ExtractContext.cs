using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Exchange.PST;

namespace Microsoft.Exchange.EDiscovery.Export
{
	// Token: 0x02000066 RID: 102
	internal class ExtractContext
	{
		// Token: 0x06000766 RID: 1894 RVA: 0x0001AC8F File Offset: 0x00018E8F
		public ExtractContext(IPST pstSession, ItemInformation item)
		{
			this.containerStack = new Stack<ExtractContext.PropertyBagContainer>();
			this.Item = item;
			this.PstSession = pstSession;
		}

		// Token: 0x1700015A RID: 346
		// (get) Token: 0x06000767 RID: 1895 RVA: 0x0001ACB0 File Offset: 0x00018EB0
		// (set) Token: 0x06000768 RID: 1896 RVA: 0x0001ACB8 File Offset: 0x00018EB8
		public ItemInformation Item { get; private set; }

		// Token: 0x1700015B RID: 347
		// (get) Token: 0x06000769 RID: 1897 RVA: 0x0001ACC1 File Offset: 0x00018EC1
		// (set) Token: 0x0600076A RID: 1898 RVA: 0x0001ACC9 File Offset: 0x00018EC9
		public IPST PstSession { get; private set; }

		// Token: 0x1700015C RID: 348
		// (get) Token: 0x0600076B RID: 1899 RVA: 0x0001ACD2 File Offset: 0x00018ED2
		// (set) Token: 0x0600076C RID: 1900 RVA: 0x0001ACDA File Offset: 0x00018EDA
		public IPropertyBag CurrentPropertyBag { get; private set; }

		// Token: 0x0600076D RID: 1901 RVA: 0x0001ACE3 File Offset: 0x00018EE3
		public void EnterAttachmentContext()
		{
			if (this.containerStack.Count == 0)
			{
				throw new ExportException(ExportErrorType.MessageDataCorrupted);
			}
			this.EnterContext(new ExtractContext.AttachmentWrapper(this.containerStack.Peek().AddAttachment()));
		}

		// Token: 0x0600076E RID: 1902 RVA: 0x0001AD14 File Offset: 0x00018F14
		public void EnterMessageContext(IMessage message)
		{
			if (message == null)
			{
				if (this.containerStack.Count == 0)
				{
					throw new ExportException(ExportErrorType.MessageDataCorrupted);
				}
				message = this.containerStack.Peek().AddEmbeddedMessage();
			}
			this.EnterContext(new ExtractContext.MessageWrapper(message));
		}

		// Token: 0x0600076F RID: 1903 RVA: 0x0001AD4B File Offset: 0x00018F4B
		public void EnterRecipientContext()
		{
			if (this.containerStack.Count == 0)
			{
				throw new ExportException(ExportErrorType.MessageDataCorrupted);
			}
			this.EnterContext(new ExtractContext.RecipientWrapper(this.containerStack.Peek().AddRecipient()));
		}

		// Token: 0x06000770 RID: 1904 RVA: 0x0001AD7C File Offset: 0x00018F7C
		public void ExitAttachmentContext()
		{
			this.ExitContext<ExtractContext.AttachmentWrapper>();
		}

		// Token: 0x06000771 RID: 1905 RVA: 0x0001AD84 File Offset: 0x00018F84
		public void ExitMessageContext()
		{
			this.ExitContext<ExtractContext.MessageWrapper>();
		}

		// Token: 0x06000772 RID: 1906 RVA: 0x0001AD8C File Offset: 0x00018F8C
		public void ExitRecipientContext()
		{
			this.ExitContext<ExtractContext.RecipientWrapper>();
		}

		// Token: 0x06000773 RID: 1907 RVA: 0x0001AD94 File Offset: 0x00018F94
		private void EnterContext(ExtractContext.PropertyBagContainer container)
		{
			this.containerStack.Push(container);
			this.CurrentPropertyBag = container.PropertyBag;
		}

		// Token: 0x06000774 RID: 1908 RVA: 0x0001ADB0 File Offset: 0x00018FB0
		private void ExitContext<T>() where T : ExtractContext.PropertyBagContainer
		{
			if (this.containerStack.Count > 0)
			{
				ExtractContext.PropertyBagContainer propertyBagContainer = this.containerStack.Pop();
				if (propertyBagContainer is T)
				{
					propertyBagContainer.Save();
					if (this.containerStack.Count > 0)
					{
						this.CurrentPropertyBag = this.containerStack.Peek().PropertyBag;
						return;
					}
					this.CurrentPropertyBag = null;
					return;
				}
			}
			throw new ExportException(ExportErrorType.MessageDataCorrupted);
		}

		// Token: 0x04000280 RID: 640
		private Stack<ExtractContext.PropertyBagContainer> containerStack;

		// Token: 0x02000067 RID: 103
		private abstract class PropertyBagContainer
		{
			// Token: 0x1700015D RID: 349
			// (get) Token: 0x06000775 RID: 1909
			public abstract IPropertyBag PropertyBag { get; }

			// Token: 0x06000776 RID: 1910 RVA: 0x0001AE18 File Offset: 0x00019018
			public virtual void Save()
			{
			}

			// Token: 0x06000777 RID: 1911 RVA: 0x0001AE1A File Offset: 0x0001901A
			public virtual IMessage AddEmbeddedMessage()
			{
				throw new ExportException(ExportErrorType.MessageDataCorrupted);
			}

			// Token: 0x06000778 RID: 1912 RVA: 0x0001AE22 File Offset: 0x00019022
			public virtual IPropertyBag AddRecipient()
			{
				throw new ExportException(ExportErrorType.MessageDataCorrupted);
			}

			// Token: 0x06000779 RID: 1913 RVA: 0x0001AE2A File Offset: 0x0001902A
			public virtual IAttachment AddAttachment()
			{
				throw new ExportException(ExportErrorType.MessageDataCorrupted);
			}
		}

		// Token: 0x02000068 RID: 104
		private class AttachmentWrapper : ExtractContext.PropertyBagContainer
		{
			// Token: 0x0600077B RID: 1915 RVA: 0x0001AE3A File Offset: 0x0001903A
			public AttachmentWrapper(IAttachment attachment)
			{
				this.attachment = attachment;
			}

			// Token: 0x1700015E RID: 350
			// (get) Token: 0x0600077C RID: 1916 RVA: 0x0001AE49 File Offset: 0x00019049
			public override IPropertyBag PropertyBag
			{
				get
				{
					return this.attachment.PropertyBag;
				}
			}

			// Token: 0x0600077D RID: 1917 RVA: 0x0001AE56 File Offset: 0x00019056
			public override void Save()
			{
				this.attachment.Save();
			}

			// Token: 0x0600077E RID: 1918 RVA: 0x0001AE64 File Offset: 0x00019064
			public override IMessage AddEmbeddedMessage()
			{
				IMessage message = this.attachment.AddMessageAttachment();
				IProperty property = this.attachment.PropertyBag.AddProperty(922812429U);
				IPropertyWriter propertyWriter = property.OpenStreamWriter();
				propertyWriter.Write(BitConverter.GetBytes((ulong)message.Id));
				propertyWriter.Close();
				return message;
			}

			// Token: 0x04000284 RID: 644
			private IAttachment attachment;
		}

		// Token: 0x02000069 RID: 105
		private class MessageWrapper : ExtractContext.PropertyBagContainer
		{
			// Token: 0x0600077F RID: 1919 RVA: 0x0001AEB3 File Offset: 0x000190B3
			public MessageWrapper(IMessage message)
			{
				this.message = message;
				this.recipients = new List<IPropertyBag>();
			}

			// Token: 0x1700015F RID: 351
			// (get) Token: 0x06000780 RID: 1920 RVA: 0x0001AECD File Offset: 0x000190CD
			public override IPropertyBag PropertyBag
			{
				get
				{
					return this.message.PropertyBag;
				}
			}

			// Token: 0x06000781 RID: 1921 RVA: 0x0001AEDC File Offset: 0x000190DC
			public override void Save()
			{
				StringBuilder stringBuilder = new StringBuilder();
				StringBuilder stringBuilder2 = new StringBuilder();
				StringBuilder stringBuilder3 = new StringBuilder();
				foreach (IPropertyBag propertyBag in this.recipients)
				{
					IProperty property = null;
					IProperty property2 = null;
					if (propertyBag.Properties.TryGetValue(PropertyTag.RecipientType.Id, out property) && property != null && propertyBag.Properties.TryGetValue(PropertyTag.DisplayName.Id, out property2) && property2 != null)
					{
						IPropertyReader propertyReader = property.OpenStreamReader();
						byte[] value = propertyReader.Read();
						uint num = BitConverter.ToUInt32(value, 0);
						propertyReader.Close();
						IPropertyReader propertyReader2 = property2.OpenStreamReader();
						byte[] bytes = propertyReader2.Read();
						string @string = Encoding.Unicode.GetString(bytes);
						propertyReader2.Close();
						switch (num)
						{
						case 1U:
							stringBuilder.Append(@string);
							stringBuilder.Append(';');
							break;
						case 2U:
							stringBuilder2.Append(@string);
							stringBuilder2.Append(';');
							break;
						case 3U:
							stringBuilder3.Append(@string);
							stringBuilder3.Append(';');
							break;
						}
					}
				}
				this.AddRecipientDisplayProperty(PropertyTag.DisplayTo, stringBuilder);
				this.AddRecipientDisplayProperty(PropertyTag.DisplayCc, stringBuilder2);
				this.AddRecipientDisplayProperty(PropertyTag.DisplayBcc, stringBuilder3);
				this.recipients.Clear();
				this.message.Save();
			}

			// Token: 0x06000782 RID: 1922 RVA: 0x0001B07C File Offset: 0x0001927C
			public override IAttachment AddAttachment()
			{
				return this.message.AddAttachment();
			}

			// Token: 0x06000783 RID: 1923 RVA: 0x0001B08C File Offset: 0x0001928C
			public override IPropertyBag AddRecipient()
			{
				IPropertyBag propertyBag = this.message.AddRecipient();
				this.recipients.Add(propertyBag);
				return propertyBag;
			}

			// Token: 0x06000784 RID: 1924 RVA: 0x0001B0B4 File Offset: 0x000192B4
			private void AddRecipientDisplayProperty(PropertyTag propertyTag, StringBuilder displayString)
			{
				if (displayString.Length > 0)
				{
					IProperty property = this.PropertyBag.AddProperty(propertyTag.NormalizedValueForPst);
					IPropertyWriter propertyWriter = property.OpenStreamWriter();
					propertyWriter.Write(Encoding.Unicode.GetBytes(displayString.ToString(0, displayString.Length - 1)));
					propertyWriter.Close();
				}
			}

			// Token: 0x04000285 RID: 645
			private IMessage message;

			// Token: 0x04000286 RID: 646
			private List<IPropertyBag> recipients;
		}

		// Token: 0x0200006A RID: 106
		private class RecipientWrapper : ExtractContext.PropertyBagContainer
		{
			// Token: 0x06000785 RID: 1925 RVA: 0x0001B109 File Offset: 0x00019309
			public RecipientWrapper(IPropertyBag recipient)
			{
				this.recipient = recipient;
			}

			// Token: 0x17000160 RID: 352
			// (get) Token: 0x06000786 RID: 1926 RVA: 0x0001B118 File Offset: 0x00019318
			public override IPropertyBag PropertyBag
			{
				get
				{
					return this.recipient;
				}
			}

			// Token: 0x04000287 RID: 647
			private IPropertyBag recipient;
		}
	}
}
