/* generated using openapi-typescript-codegen -- do no edit */
/* istanbul ignore file */
/* tslint:disable */
/* eslint-disable */
import type { EntryOverview } from '../models/EntryOverview';

import type { CancelablePromise } from '../core/CancelablePromise';
import { OpenAPI } from '../core/OpenAPI';
import { request as __request } from '../core/request';

export class GetEntryOverviewHandlerService {

    /**
     * @returns EntryOverview Success
     * @throws ApiError
     */
    public static getApiEntry(): CancelablePromise<Array<EntryOverview>> {
        return __request(OpenAPI, {
            method: 'GET',
            url: '/api/entry',
        });
    }

}
