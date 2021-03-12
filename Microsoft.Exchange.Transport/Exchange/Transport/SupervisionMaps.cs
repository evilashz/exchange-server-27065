using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Diagnostics.Components.Transport;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x020001A1 RID: 417
	internal sealed class SupervisionMaps
	{
		// Token: 0x060011F7 RID: 4599 RVA: 0x00049508 File Offset: 0x00047708
		public SupervisionMaps(ADRawEntry entry, IList<string> requiredTags)
		{
			if (entry == null)
			{
				throw new ArgumentNullException("entry");
			}
			if (requiredTags == null)
			{
				throw new ArgumentNullException("requiredTags");
			}
			ExTraceGlobals.SupervisionTracer.TraceDebug(0L, "Creating supervision maps for {0}", new object[]
			{
				entry[ADObjectSchema.DistinguishedName]
			});
			this.internalRecipientSupervisionMap = new Dictionary<string, List<ADObjectId>>(requiredTags.Count, StringComparer.OrdinalIgnoreCase);
			this.dlSupervisionMap = new Dictionary<string, List<ADObjectId>>(requiredTags.Count, StringComparer.OrdinalIgnoreCase);
			this.oneOffSupervisionMap = new Dictionary<string, List<SmtpAddress>>(requiredTags.Count, StringComparer.OrdinalIgnoreCase);
			foreach (string key in requiredTags)
			{
				this.internalRecipientSupervisionMap.Add(key, new List<ADObjectId>());
				this.dlSupervisionMap.Add(key, new List<ADObjectId>());
				this.oneOffSupervisionMap.Add(key, new List<SmtpAddress>());
			}
			this.ConstructRecipientSupervisionMap(entry, true);
			this.ConstructRecipientSupervisionMap(entry, false);
			this.ConstructOneOffSupervisionMap(entry);
			ExTraceGlobals.SupervisionTracer.TraceDebug(0L, "Supervision maps for {0} created", new object[]
			{
				entry[ADObjectSchema.DistinguishedName]
			});
		}

		// Token: 0x170004B8 RID: 1208
		// (get) Token: 0x060011F8 RID: 4600 RVA: 0x00049648 File Offset: 0x00047848
		public Dictionary<string, List<ADObjectId>> InternalRecipientSupervisionMap
		{
			get
			{
				return this.internalRecipientSupervisionMap;
			}
		}

		// Token: 0x170004B9 RID: 1209
		// (get) Token: 0x060011F9 RID: 4601 RVA: 0x00049650 File Offset: 0x00047850
		public Dictionary<string, List<ADObjectId>> DlSupervisionMap
		{
			get
			{
				return this.dlSupervisionMap;
			}
		}

		// Token: 0x170004BA RID: 1210
		// (get) Token: 0x060011FA RID: 4602 RVA: 0x00049658 File Offset: 0x00047858
		public Dictionary<string, List<SmtpAddress>> OneOffSupervisionMap
		{
			get
			{
				return this.oneOffSupervisionMap;
			}
		}

		// Token: 0x060011FB RID: 4603 RVA: 0x00049660 File Offset: 0x00047860
		private void ConstructRecipientSupervisionMap(ADRawEntry entry, bool internalRecipient)
		{
			MultiValuedProperty<ADObjectIdWithString> multiValuedProperty;
			Dictionary<string, List<ADObjectId>> dictionary;
			if (internalRecipient)
			{
				multiValuedProperty = (MultiValuedProperty<ADObjectIdWithString>)entry[ADRecipientSchema.InternalRecipientSupervisionList];
				dictionary = this.internalRecipientSupervisionMap;
			}
			else
			{
				multiValuedProperty = (MultiValuedProperty<ADObjectIdWithString>)entry[ADRecipientSchema.DLSupervisionList];
				dictionary = this.dlSupervisionMap;
			}
			SupervisionListEntryConstraint supervisionListEntryConstraint = new SupervisionListEntryConstraint(false);
			foreach (ADObjectIdWithString adobjectIdWithString in multiValuedProperty)
			{
				PropertyConstraintViolationError propertyConstraintViolationError = supervisionListEntryConstraint.Validate(adobjectIdWithString, null, null);
				if (propertyConstraintViolationError != null)
				{
					ExTraceGlobals.SupervisionTracer.TraceDebug<string, ADObjectIdWithString, LocalizedString>(0L, "Ignoring {0} supervision list entry {1} due to validation error {2}", internalRecipient ? "internal recipient" : "DL", adobjectIdWithString, propertyConstraintViolationError.Description);
				}
				else
				{
					string[] array = adobjectIdWithString.StringValue.Split(new char[]
					{
						SupervisionListEntryConstraint.Delimiter
					});
					foreach (string text in array)
					{
						if (!text.Equals(string.Empty) && dictionary.ContainsKey(text))
						{
							List<ADObjectId> list = dictionary[text];
							if (!list.Contains(adobjectIdWithString.ObjectIdValue))
							{
								list.Add(adobjectIdWithString.ObjectIdValue);
							}
						}
					}
				}
			}
		}

		// Token: 0x060011FC RID: 4604 RVA: 0x000497A0 File Offset: 0x000479A0
		private void ConstructOneOffSupervisionMap(ADRawEntry entry)
		{
			MultiValuedProperty<ADObjectIdWithString> multiValuedProperty = (MultiValuedProperty<ADObjectIdWithString>)entry[ADRecipientSchema.OneOffSupervisionList];
			SupervisionListEntryConstraint supervisionListEntryConstraint = new SupervisionListEntryConstraint(true);
			foreach (ADObjectIdWithString adobjectIdWithString in multiValuedProperty)
			{
				PropertyConstraintViolationError propertyConstraintViolationError = supervisionListEntryConstraint.Validate(adobjectIdWithString, null, null);
				if (propertyConstraintViolationError != null)
				{
					ExTraceGlobals.SupervisionTracer.TraceDebug<ADObjectIdWithString, LocalizedString>(0L, "Ignoring one off supervision list entry {0} due to validation error {1}", adobjectIdWithString, propertyConstraintViolationError.Description);
				}
				else
				{
					string[] array = adobjectIdWithString.StringValue.Split(new char[]
					{
						SupervisionListEntryConstraint.Delimiter
					});
					SmtpAddress item = new SmtpAddress(array[array.Length - 1]);
					for (int i = 0; i < array.Length - 1; i++)
					{
						string text = array[i];
						if (!text.Equals(string.Empty) && this.oneOffSupervisionMap.ContainsKey(text))
						{
							List<SmtpAddress> list = this.oneOffSupervisionMap[text];
							if (!list.Contains(item))
							{
								list.Add(item);
							}
						}
					}
				}
			}
		}

		// Token: 0x04000985 RID: 2437
		private Dictionary<string, List<ADObjectId>> internalRecipientSupervisionMap;

		// Token: 0x04000986 RID: 2438
		private Dictionary<string, List<ADObjectId>> dlSupervisionMap;

		// Token: 0x04000987 RID: 2439
		private Dictionary<string, List<SmtpAddress>> oneOffSupervisionMap;
	}
}
