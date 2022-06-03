import axios from 'axios'
import HttpMethods from 'http-methods-constants'
import StringUtil from '@/utils/string-util'

class HttpUtil {
  constructor({timeout = 10000, baseURL = null} = {}) {
    this.axios = axios.create({timeout})
    if (!StringUtil.isNullOrEmpty(baseURL)) {
      this.axios.defaults.baseURL = baseURL
    }
  }

  makeHTTPRequest({
                    url,
                    payload,
                    method = HttpMethods.GET
                  }) {

    let requestOptions = {url, method}

    if (method === HttpMethods.GET) {
      requestOptions.params = payload
    } else {
      requestOptions.data = payload
    }

    return new Promise((resolve, reject) => {
      this.axios.request(requestOptions)
        .then(res => {
          if (res.data.status !== 0) reject(res.data)
          else resolve(res.data)
        })
        .catch(err => reject(err.response.data))
    })
  }
}

export default {
  instance: new HttpUtil({baseURL: process.env.VUE_APP_API_BASE_URL})
}
