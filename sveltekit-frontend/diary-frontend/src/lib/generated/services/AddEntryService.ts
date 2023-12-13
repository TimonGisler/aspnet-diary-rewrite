/* generated using openapi-typescript-codegen -- do no edit */
/* istanbul ignore file */
/* tslint:disable */
/* eslint-disable */
import type { SaveEntryCommand } from '../models/SaveEntryCommand';
import type { SaveEntryResponse } from '../models/SaveEntryResponse';

import type { CancelablePromise } from '../core/CancelablePromise';
import { OpenAPI } from '../core/OpenAPI';
import { request as __request } from '../core/request';

export class AddEntryService {

    /**
     * @param requestBody 
     * @returns SaveEntryResponse Success
     * @throws ApiError
     */
    public static postApiEntry(
requestBody: SaveEntryCommand,
): CancelablePromise<SaveEntryResponse> {
        return __request(OpenAPI, {
            method: 'POST',
            url: '/api/entry',
            body: requestBody,
            mediaType: 'application/json',
        });
    }

}
