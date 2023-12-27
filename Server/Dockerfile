FROM jellydn/alpine-nodejs:18 AS build
WORKDIR /srv
ADD package.json .
RUN npm install
ADD . .

FROM jellydn/alpine-nodejs:slim-18
COPY --from=build /srv .
EXPOSE 3000
CMD ["node", "server.js"]
