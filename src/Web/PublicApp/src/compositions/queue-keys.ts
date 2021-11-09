import axios from "@/utils/axios";
import { Device } from "@capacitor/device";
import { useReCaptcha } from "vue-recaptcha-v3";
import { useNetwork } from "@/compositions/network";

interface CreateQueueKeyRequest {
	deviceId: string;
}

export function useQueueKeys() {
	const network = useNetwork();
	// eslint-disable-next-line @typescript-eslint/no-non-null-assertion
	const { executeRecaptcha, recaptchaLoaded } = useReCaptcha()!;
	const key = ref<QueueKey | undefined>();

	async function createKey(): Promise<QueueKey | undefined> {
		if (!network.status.value?.connected) return;

		await recaptchaLoaded();

		const reCaptchaToken = await executeRecaptcha("queue_key");

		const id = await Device.getId();
		const data: CreateQueueKeyRequest = { deviceId: id.uuid };
		const resp = await axios.post("queue-keys", data, {
			headers: {
				["X-ReCAPTCHA-V3"]: reCaptchaToken,
			},
		});

		return (resp.status == 200 && (resp.data as QueueKey)) || undefined;
	}

	watch(network.status, createKey);

	return { key, createKey };
}
