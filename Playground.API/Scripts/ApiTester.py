import asyncio
import aiohttp
import time

URL = "http://localhost:5282/api/currency"

async def fetch(session, i):
    async with session.get(URL) as resp:
        print(f"Request {i} status: {resp.status}")
        await resp.text()

async def run():
    async with aiohttp.ClientSession() as session:
        tasks = [fetch(session, i) for i in range(100)]
        await asyncio.gather(*tasks)

start = time.time()
asyncio.run(run())
print(f"Completed in {time.time() - start:.2f} seconds")
